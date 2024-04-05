using System.Text.Json.Serialization;
public class ExecuteContextApp
{
    [JsonPropertyName("anonymous")]
    public bool? Anonymous { get; set; }
    [JsonPropertyName("id")]
    public int? Id { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
