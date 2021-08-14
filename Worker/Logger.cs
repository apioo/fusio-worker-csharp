
using System.Collections.Generic;

namespace FusioWorker
{
    public class Logger
    {
        private readonly List<Log> Logs = new List<Log>();

        public void Emergency(string message)
        {
            this.Log("EMERGENCY", message);
        }

        public void Alert(string message)
        {
            this.Log("ALERT", message);
        }

        public void Critical(string message)
        {
            this.Log("CRITICAL", message);
        }

        public void Error(string message)
        {
            this.Log("ERROR", message);
        }

        public void Warning(string message)
        {
            this.Log("WARNING", message);
        }

        public void Notice(string message)
        {
            this.Log("NOTICE", message);
        }

        public void Info(string message)
        {
            this.Log("INFO", message);
        }

        public void Debug(string message)
        {
            this.Log("DEBUG", message);
        }

        private void Log(string level, string message)
        {
            Log log = new Log
            {
                Level = level,
                Message = message
            };

            this.Logs.Add(log);
        }

        public List<Log> GetLogs()
        {
            return this.Logs;
        }
    }
}
