using System.Text.Json.Serialization;
public class About
{
    [JsonPropertyName("apiVersion")]
    public string? ApiVersion { get; set; }
    [JsonPropertyName("language")]
    public string? Language { get; set; }
}
