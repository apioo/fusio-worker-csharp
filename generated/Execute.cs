using System.Text.Json.Serialization;
using System.Collections.Generic;
public class Execute
{
    [JsonPropertyName("connections")]
    public Dictionary<string, ExecuteConnection>? Connections { get; set; }
    [JsonPropertyName("request")]
    public ExecuteRequest? Request { get; set; }
    [JsonPropertyName("context")]
    public ExecuteContext? Context { get; set; }
}
