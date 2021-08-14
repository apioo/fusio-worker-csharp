using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
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


}
