using System.Text.Json.Serialization;
public class Message
{
    [JsonPropertyName("success")]
    public bool? Success { get; set; }
    [JsonPropertyName("message")]
    public string? Message { get; set; }
    [JsonPropertyName("trace")]
    public string? Trace { get; set; }
}
