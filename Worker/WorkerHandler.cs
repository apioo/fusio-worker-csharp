
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CSharp;
using Microsoft.Extensions.Logging;
using System;
using System.CodeDom.Compiler;
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

        private FusioWorker.Connector.Connections connections = null;
        private readonly ILogger _logger;

        public Task<Message> setConnectionAsync(Connection connection, CancellationToken cancellationToken = default)
        {
            if (!Directory.Exists(WorkerHandler.ACTION_DIR))
            {
                Directory.CreateDirectory(WorkerHandler.ACTION_DIR);
            }

            Message msg;
            FusioWorker.Connector.Connections connections = this.ReadConnections();

            if (String.IsNullOrEmpty(connection.Name))
            {
                msg = new Message();
                msg.Success = false;
                msg.Message_ = "Provided no connection name";

                return Task.FromResult(msg);
            }

            FusioWorker.Connector.Connection conf = new FusioWorker.Connector.Connection
            {
                Type = connection.Type,
                Config = connection.Config
            };
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
            Connector connector = new Connector(this.ReadConnections());
            Dispatcher dispatcher = new Dispatcher();
            Logger logger = new Logger();

            if (String.IsNullOrEmpty(execute.Action)) {
                return null;
            }

            this._logger.LogInformation("Execute action " + execute.Action);

            string file = WorkerHandler.ACTION_DIR + "/" + execute.Action + ".cs";

            try
            {
                IAction action = this.CreateActionInstance(file);

                action.Handle();
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

                Result result = new Result
                {
                    Response = resp
                };

                return Task.FromResult(result);
            }
        }

        private FusioWorker.Connector.Connections ReadConnections()
        {
            return new FusioWorker.Connector.Connections();
        }

        //
        // @see https://gist.github.com/RickStrahl/f65727881668488b0a562df4c21ab560
        //
        private IAction CreateActionInstance(string actionName, string file)
        {
            var options = ScriptOptions.Default;
            options.AddImports("System");
            options.AddImports("FusioWorker.Generated");

            var sourceCode = File.ReadAllText(file);

            var finalScript = CSharpScript.Create(sourceCode, options, typeof(FusioAPI));
            finalScript.Compile();

            Compilation compilation = finalScript.GetCompilation();

            SyntaxTree syntaxTree = compilation.SyntaxTrees.First();
            syntaxRootNode = syntaxTree.GetRoot() as CompilationUnitSyntax;
            semanticModel = compilation.GetSemanticModel(compilation.SyntaxTrees.First());

            ScriptState<object> result = await finalScript.RunAsync(new FusioAPI());

            var result = CSharpScript.EvaluateAsync(sourceCode, opt);


            if (inst is IAction)
            {
                return inst;
            }
            else
            {
                throw new Exception("Action is not an instance of IAction");
            }
        }
    }

    public class FusioAPI
    {
        public void Connector(string input)
        {
            Console.WriteLine(input);
        }
    }
}
