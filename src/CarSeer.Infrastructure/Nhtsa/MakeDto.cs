using System.Text.Json.Serialization;

namespace CarSeer.Infrastructure.Nhtsa;

internal sealed class MakeDto
{
    [JsonPropertyName("Make_ID")]
    public int MakeId { get; init; }

    [JsonPropertyName("Make_Name")]
    public string MakeName { get; init; } = string.Empty;
}
