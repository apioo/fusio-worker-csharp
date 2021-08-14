
using FusioWorker.Connector;
using FusioWorker.Generated;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace FusioWorker
{
    public class WorkerHandler : Worker.IAsync
    {
        private readonly static string ACTION_DIR = "./actions";

        private Connections connections = null;
        private ILogger logger;

        public WorkerHandler(ILogger logger)
        {
            this.logger = logger;

            this.logger.LogInformation("Create logger");
        }

        public Task<Message> setConnectionAsync(Generated.Connection connection, CancellationToken cancellationToken = default)
        {
            this.logger.LogInformation("call setConnectionAsync");

            if (!Directory.Exists(WorkerHandler.ACTION_DIR))
            {
                Directory.CreateDirectory(WorkerHandler.ACTION_DIR);
            }

            Message msg;
            Connections connections = this.ReadConnections();

            if (String.IsNullOrEmpty(connection.Name))
            {
                msg = new Message();
                msg.Success = false;
                msg.Message_ = "Provided no connection name";

                return Task.FromResult(msg);
            }

            Connector.Connection conf = new Connector.Connection
            {
                Type = connection.Type,
                Config = connection.Config
            };
            connections.Add(connection.Name, conf);

            using FileStream createStream = File.Create(WorkerHandler.ACTION_DIR + "/connections.json");
            JsonSerializer.SerializeAsync(createStream, connections);

            // reset connections
            this.connections = null;

            this.logger.LogInformation("Update connection " + connection.Name);

            msg = new Message();
            msg.Success = true;
            msg.Message_ = "Connection successful updated";

            return Task.FromResult(msg);
        }

        public Task<Message> setActionAsync(Generated.Action action, CancellationToken cancellationToken = default)
        {
            this.logger.LogInformation("call setActionAsync");

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

            this.logger.LogInformation("Update action " + action.Name);

            msg = new Message();
            msg.Success = true;
            msg.Message_ = "Action successful updated";

            return Task.FromResult(msg);
        }

        public Task<Result> executeActionAsync(Execute execute, CancellationToken cancellationToken = default)
        {
            this.logger.LogInformation("call executeActionAsync");

            Connector.Connector connector = new Connector.Connector(this.ReadConnections());
            Dispatcher dispatcher = new Dispatcher();
            Logger logger = new Logger();
            Result result;

            if (String.IsNullOrEmpty(execute.Action)) {
                return null;
            }

            this.logger.LogInformation("Execute action " + execute.Action);

            string file = WorkerHandler.ACTION_DIR + "/" + execute.Action + ".csx";

            try
            {
                var sourceCode = File.ReadAllText(file);

                var options = ScriptOptions.Default;
                options.AddImports("FusioWorker.Generated");

                var api = new FusioAPI(connector, dispatcher, logger);

                Response resp = null;
                CSharpScript.EvaluateAsync<Response>(sourceCode, options, api)
                    .ContinueWith(s => resp = s.Result)
                    .Wait();

                if (resp == null)
                {
                    throw new Exception("Script does not return a response");
                }

                result = new Result
                {
                    Response = resp,
                    Events = dispatcher.GetEvents(),
                    Logs = logger.GetLogs()
                };

                return Task.FromResult(result);
            }
            catch(Exception e)
            {
                Dictionary<string, object> body = new Dictionary<string, object>
                {
                    { "success", false },
                    { "message", "An error occurred at the worker: " + e.ToString() }
                };

                Response resp = new Response
                {
                    StatusCode = 500,
                    Headers = new Dictionary<string, string>(),
                    Body = JsonSerializer.Serialize(body)
                };

                result = new Result
                {
                    Response = resp
                };

                return Task.FromResult(result);
            }
        }

        private Connections ReadConnections()
        {
            if (this.connections != null)
            {
                return this.connections;
            }

            string file = WorkerHandler.ACTION_DIR + "/connections.json";
            if (File.Exists(file))
            {
                this.connections = (Connections)JsonSerializer.Deserialize(File.ReadAllText(file), typeof(Connections));
            }

            return this.connections != null ? this.connections : new Connections();
        }
    }

    public class FusioAPI
    {
        public readonly Connector.Connector connector;
        public readonly Dispatcher dispatcher;
        public readonly Logger logger;
        public readonly ResponseBuilder response;
        
        public FusioAPI(Connector.Connector connector, Dispatcher dispatcher, Logger logger)
        {
            this.connector = connector;
            this.dispatcher = dispatcher;
            this.logger = logger;
            this.response = new ResponseBuilder();
        }
    }
}
