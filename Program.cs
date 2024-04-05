using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

var actionsDir = "./actions";
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseExceptionHandler(exceptionHandlerApp 
    => exceptionHandlerApp.Run(async context 
        => {
            var feature = context.Features.Get<IExceptionHandlerFeature>();

            var message = new Message {
                Success = false,
                _Message = feature != null ? feature.Error.Message : "An unknown error occurred",
                Trace = feature != null ? feature.Error.StackTrace : ""
            };

            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync<Message>(message);
        }));

app.MapGet("/", () => {
    return new About {
        ApiVersion = "1.0.0",
        Language = "csharp"
    };
});

app.MapPost("/{action}", async (string action) => {
    var connector = new Connector();
    var dispatcher = new Dispatcher();
    var logger = new Logger();
    var responseBuilder = new ResponseBuilder();

    var file = GetActionFile(action);
    var runtime = new Runtime() { Connector = connector, Dispatcher = dispatcher, Logger = logger, Response = responseBuilder };

    var options = ScriptOptions.Default;
    options.AddImports(["System.Collections.Generic", ""]);
    options = options.WithAllowUnsafe(true);
    var code = File.ReadAllText(file);
    Script script = CSharpScript.Create(code, options, globalsType: typeof(Runtime));
    script.Compile();

    await script.RunAsync(runtime);

    ResponseHTTP? response = responseBuilder.GetResponse();
    if (response == null) {
        response = new ResponseHTTP();
        response.StatusCode = 204;
    }

    return new Response {
        Events = dispatcher.GetEvents(),
        Logs = logger.GetLogs(),
        _Response = response
    };
});

app.MapPut("/{action}", (string action, Update update) => {
    if (!Directory.Exists(actionsDir)) {
        Directory.CreateDirectory(actionsDir);
    }

    var file = GetActionFile(action);

    File.WriteAllText(file, update.Code);

    return new Message {
        Success = true,
        _Message = "Action successfully updated",
    };
});

app.MapDelete("/{action}", (string action) => {
    if (!Directory.Exists(actionsDir)) {
        Directory.CreateDirectory(actionsDir);
    }

    var file = GetActionFile(action);

    if (File.Exists(file)) {
        File.Delete(file);
    }

    return new Message {
        Success = true,
        _Message = "Action successfully deleted",
    };
});

app.Run();


string GetActionFile(string action)
{
    Regex regex = new("^[A-Za-z0-9_-]{3,30}$");
    if (!regex.IsMatch(action)) {
        throw new Exception("Provided no valid action name");
    }

    return actionsDir + "/" + action + ".csx";
}

