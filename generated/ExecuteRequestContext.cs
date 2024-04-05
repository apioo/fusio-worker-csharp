using System.Text.Json.Serialization;
using System.Collections.Generic;
public class ExecuteRequestContext
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }
    [JsonPropertyName("uriFragments")]
    public Dictionary<string, string>? UriFragments { get; set; }
    [JsonPropertyName("method")]
    public string? Method { get; set; }
    [JsonPropertyName("path")]
    public string? Path { get; set; }
    [JsonPropertyName("queryParameters")]
    public Dictionary<string, string>? QueryParameters { get; set; }
    [JsonPropertyName("headers")]
    public Dictionary<string, string>? Headers { get; set; }
}
