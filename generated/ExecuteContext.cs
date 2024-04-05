using System.Text.Json.Serialization;
public class ExecuteContext
{
    [JsonPropertyName("operationId")]
    public int? OperationId { get; set; }
    [JsonPropertyName("baseUrl")]
    public string? BaseUrl { get; set; }
    [JsonPropertyName("tenantId")]
    public string? TenantId { get; set; }
    [JsonPropertyName("action")]
    public string? Action { get; set; }
    [JsonPropertyName("app")]
    public ExecuteContextApp? App { get; set; }
    [JsonPropertyName("user")]
    public ExecuteContextUser? User { get; set; }
}
