
class Logger
{
    private List<ResponseLog> logs = new();

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

    public void Log(string level, string message)
    {
        ResponseLog log = new();
        log.Level = level;
        log.Message = message;

        this.logs.Add(log);
    }

    public List<ResponseLog> GetLogs()
    {
        return this.logs;
    }
}
