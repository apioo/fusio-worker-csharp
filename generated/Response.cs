using System.Text.Json.Serialization;
public class Response
{
    [JsonPropertyName("response")]
    public ResponseHTTP? Response { get; set; }
    [JsonPropertyName("events")]
    public List<ResponseEvent>? Events { get; set; }
    [JsonPropertyName("logs")]
    public List<ResponseLog>? Logs { get; set; }
}
