
using System.Collections.Generic;
using System.Text.Json;

namespace FusioWorker
{
    public class Dispatcher
    {
        private readonly List<Event> events = new List<Event>();

        public void Dispatch(string eventName, object data)
        {
            Event ev = new Event
            {
                EventName = eventName,
                Data = JsonSerializer.Serialize(data)
            };

            this.events.Add(ev);
        }

        public List<Event> GetEvents()
        {
            return this.events;
        }
    }
}
