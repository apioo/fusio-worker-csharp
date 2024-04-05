using System.Text.Json.Serialization;
public class ResponseEvent
{
    [JsonPropertyName("eventName")]
    public string? EventName { get; set; }
    [JsonPropertyName("data")]
    public object? Data { get; set; }
}
