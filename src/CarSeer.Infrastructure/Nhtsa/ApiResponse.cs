using System.Text.Json.Serialization;

namespace CarSeer.Infrastructure.Nhtsa;

internal sealed class ApiResponse<T>
{
    [JsonPropertyName("Count")]
    public int Count { get; init; }

    [JsonPropertyName("Message")]
    public string? Message { get; init; }

    [JsonPropertyName("SearchCriteria")]
    public string? SearchCriteria { get; init; }

    [JsonPropertyName("Results")]
    public List<T> Results { get; init; } = [];
}
