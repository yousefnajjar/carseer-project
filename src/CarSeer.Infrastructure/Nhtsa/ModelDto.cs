using System.Text.Json.Serialization;

namespace CarSeer.Infrastructure.Nhtsa;

internal sealed class ModelDto
{
    [JsonPropertyName("Make_ID")]
    public int MakeId { get; init; }

    [JsonPropertyName("Make_Name")]
    public string MakeName { get; init; } = string.Empty;

    [JsonPropertyName("Model_ID")]
    public int ModelId { get; init; }

    [JsonPropertyName("Model_Name")]
    public string ModelName { get; init; } = string.Empty;

    [JsonPropertyName("VehicleTypeId")]
    public int? VehicleTypeId { get; init; }

    [JsonPropertyName("VehicleTypeName")]
    public string? VehicleTypeName { get; init; }
}
