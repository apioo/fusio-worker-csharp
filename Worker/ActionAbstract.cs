
namespace FusioWorker
{
    abstract class ActionAbstract
    {
        protected Connector.Connector connector;
        protected Dispatcher dispatcher;
        protected Logger logger;
        protected ResponseBuilder response;

        public ActionAbstract(Connector.Connector connector, Dispatcher dispatcher, Logger logger)
        {
            this.connector = connector;
            this.dispatcher = dispatcher;
            this.logger = logger;
            this.response = new ResponseBuilder();
        }
    }
}

