using System.Text.Json.Serialization;
public class ExecuteConnection
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }
    [JsonPropertyName("config")]
    public string? Config { get; set; }
}
