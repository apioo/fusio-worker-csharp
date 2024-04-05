using System.Text.Json.Serialization;
public class ResponseLog
{
    [JsonPropertyName("level")]
    public string? Level { get; set; }
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
