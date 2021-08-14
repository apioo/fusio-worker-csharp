
using System.Collections.Generic;

namespace FusioWorker.Connector
{
    public class Connection
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