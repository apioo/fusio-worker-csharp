using System.Text.Json.Serialization;
public class ExecuteContextUser
{
    [JsonPropertyName("anonymous")]
    public bool? Anonymous { get; set; }
    [JsonPropertyName("id")]
    public int? Id { get; set; }
    [JsonPropertyName("planId")]
    public string? PlanId { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    [JsonPropertyName("points")]
    public int? Points { get; set; }
}
