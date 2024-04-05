using System.Text.Json.Serialization;
using System.Collections.Generic;
public class ResponseHTTP
{
    [JsonPropertyName("statusCode")]
    public int? StatusCode { get; set; }
    [JsonPropertyName("headers")]
    public Dictionary<string, string>? Headers { get; set; }
    [JsonPropertyName("body")]
    public object? Body { get; set; }
}
