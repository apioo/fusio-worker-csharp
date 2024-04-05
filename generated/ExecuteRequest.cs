using System.Text.Json.Serialization;
using System.Collections.Generic;
public class ExecuteRequest
{
    [JsonPropertyName("arguments")]
    public Dictionary<string, string>? Arguments { get; set; }
    [JsonPropertyName("payload")]
    public object? Payload { get; set; }
    [JsonPropertyName("context")]
    public ExecuteRequestContext? Context { get; set; }
}
