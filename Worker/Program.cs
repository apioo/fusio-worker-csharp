using FusioWorker.Generated;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Thrift;
using Thrift.Processor;
using Thrift.Protocol;
using Thrift.Server;
using Thrift.Transport;
using Thrift.Transport.Server;

namespace FusioWorker
{
    public class Program
    {
        private static readonly ServiceCollection serviceCollection = new ServiceCollection();
        private static ILogger logger;

        public static void Main(string[] args)
        {
            serviceCollection.AddLogging(logging => ConfigureLogging(logging));
            using (var serviceProvider = serviceCollection.BuildServiceProvider())
            {
                logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger(nameof(Program));
                logger.LogInformation("Fusio Worker started");

                using (var source = new CancellationTokenSource())
                {
                    RunServer(source.Token).GetAwaiter().GetResult();

                    logger.LogInformation("Press any key to stop...");

                    Console.ReadLine();
                    source.Cancel();
                }
            }
        }

        private static async Task RunServer(CancellationToken cancellationToken)
        {
            var listener = new TcpListener(IPAddress.Parse("0.0.0.0"), 9094);

            TServerTransport serverTransport = new TServerSocketTransport(listener, new TConfiguration());
            TTransportFactory transportFactory = new TFramedTransport.Factory();
            TProtocolFactory protocolFactory = new TBinaryProtocol.Factory();

            var handler = new WorkerHandler(logger);
            ITAsyncProcessor processor = new Worker.AsyncProcessor(handler);

            var server = new TSimpleAsyncServer(
                itProcessorFactory: new TSingletonProcessorFactory(processor),
                serverTransport: serverTransport,
                inputTransportFactory: transportFactory,
                outputTransportFactory: transportFactory,
                inputProtocolFactory: protocolFactory,
                outputProtocolFactory: protocolFactory,
                logger: logger
            );

            await server.ServeAsync(cancellationToken);
        }

        private static void ConfigureLogging(ILoggingBuilder logging)
        {
            logging.SetMinimumLevel(LogLevel.Information);
            logging.AddConsole();
            logging.AddDebug();
        }
    }
}
