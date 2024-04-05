using System.Text.Json.Serialization;
public class Update
{
    [JsonPropertyName("code")]
    public string? Code { get; set; }
}
