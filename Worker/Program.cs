using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Thrift.Processor;
using Thrift.Protocol;
using Thrift.Server;
using Thrift.Transport;
using Thrift.Transport.Server;

namespace FusioWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Server>();
                });
    }

    public class Server : BackgroundService
    {
        private readonly ILogger _logger;

        public Server(ILogger<Server> logger)
        {
            this._logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            TServerTransport serverTransport = new TServerSocketTransport(9094, new Thrift.TConfiguration());
            TTransportFactory transportFactory = null;
            TProtocolFactory protocolFactory = new TBinaryProtocol.Factory();

            var handler = new WorkerHandler();
            ITAsyncProcessor processor = new Worker.AsyncProcessor(handler);

            try
            {
                var server = new TSimpleAsyncServer(
                    itProcessorFactory: new TSingletonProcessorFactory(processor),
                    serverTransport: serverTransport,
                    inputTransportFactory: transportFactory,
                    outputTransportFactory: transportFactory,
                    inputProtocolFactory: protocolFactory,
                    outputProtocolFactory: protocolFactory,
                    logger: this._logger
                );

                this._logger.LogInformation("Fusio Worker started");

                await server.ServeAsync(cancellationToken);
            }
            catch (Exception x)
            {
                this._logger.LogError("An error occurred: " + x.ToString());
            }
        }
    }

    public class WorkerHandler : Worker.IAsync
    {
        private readonly static string ACTION_DIR = "./actions";

        private Dictionary<string, ConnectionConfig> connections = null;
        private readonly ILogger _logger;

        public Task<Message> setConnectionAsync(Connection connection, CancellationToken cancellationToken = default)
        {
            if (!Directory.Exists(WorkerHandler.ACTION_DIR))
            {
                Directory.CreateDirectory(WorkerHandler.ACTION_DIR);
            }

            Message msg;
            Dictionary<string, ConnectionConfig> connections = this.ReadConnections();

            if (String.IsNullOrEmpty(connection.Name))
            {
                msg = new Message();
                msg.Success = false;
                msg.Message_ = "Provided no connection name";

                return Task.FromResult(msg);
            }

            ConnectionConfig conf = new ConnectionConfig();
            conf.Type = connection.Type;
            conf.Config = connection.Config;
            connections.Add(connection.Name, conf);

            using FileStream createStream = File.Create(WorkerHandler.ACTION_DIR + "/connections.json");
            JsonSerializer.SerializeAsync(createStream, connections);

            // reset connections
            this.connections = null;

            this._logger.LogInformation("Update connection " + connection.Name);

            msg = new Message();
            msg.Success = true;
            msg.Message_ = "Connection successful updated";

            return Task.FromResult(msg);
        }

        public Task<Message> setActionAsync(Action action, CancellationToken cancellationToken = default)
        {
            if (!Directory.Exists(WorkerHandler.ACTION_DIR))
            {
                Directory.CreateDirectory(WorkerHandler.ACTION_DIR);
            }

            Message msg;

            if (String.IsNullOrEmpty(action.Name))
            {
                msg = new Message();
                msg.Success = false;
                msg.Message_ = "Provided no action name";

                return Task.FromResult(msg);
            }

            string file = WorkerHandler.ACTION_DIR + "/" + action.Name + ".cs";
            File.WriteAllText(file, action.Code);

            // TODO optional delete cache

            this._logger.LogInformation("Update action " + action.Name);

            msg = new Message();
            msg.Success = true;
            msg.Message_ = "Action successful updated";

            return Task.FromResult(msg);
        }

        public Task<Result> executeActionAsync(Execute execute, CancellationToken cancellationToken = default)
        {
            /*
        const connector = new Connector(readConnections());
        const dispatcher = new Dispatcher();
        const logger = new Logger();
        const response = new Response((response: HttpResponse) => {
            result(null, new Result({
                response: response,
                events: dispatcher.getEvents(),
                logs: logger.getLogs(),
            }));
        });

        if (!execute.action) {
            return;
        }

        console.debug('Execute action ' + execute.action);

        const file = ACTION_DIR + '/' + execute.action + '.js';

        try {
            const action = require(file);

            action(execute.request, execute.context, connector, response, dispatcher, logger);
        } catch (error) {
            result(null, new Result({
                response: {
                    statusCode: 500,
                    headers: {},
                    body: JSON.stringify({
                        success: false,
                        message: 'An error occurred at the worker: ' + error
                    })
                },
                events: [],
                logs: []
            }));
        }
            */
            throw new NotImplementedException();
        }

        private Dictionary<string, ConnectionConfig> ReadConnections()
        {
            return new Dictionary<string, ConnectionConfig>();
        }

        private class ConnectionConfig
        {
            private string _type;
            private Dictionary<string, string> _config;

            public string Type
            {
                get => _type;
                set => _type = value;
            }

            public Dictionary<string, string> Config
            {
                get => _config;
                set => _config = value;
            }
        }
    }
}
