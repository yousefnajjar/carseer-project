using System.Text.Json.Serialization;

namespace CarSeer.Infrastructure.Nhtsa;

internal sealed class VehicleTypeDto
{
    [JsonPropertyName("VehicleTypeId")]
    public int VehicleTypeId { get; init; }

    [JsonPropertyName("VehicleTypeName")]
    public string VehicleTypeName { get; init; } = string.Empty;
}
