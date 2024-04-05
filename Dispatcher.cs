
class Dispatcher
{
    private List<ResponseEvent> events = new();

    public void Dispatch(string eventName, object data)
    {
        ResponseEvent result = new ResponseEvent();
        result.EventName = eventName;
        result.Data = data;

        this.events.Add(result);
    }

    public List<ResponseEvent> GetEvents()
    {
        return this.events;
    }
}
