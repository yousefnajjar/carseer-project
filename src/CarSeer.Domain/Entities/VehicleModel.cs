namespace CarSeer.Domain.Entities;

public sealed record VehicleModel(
    int Id,
    string Name,
    int MakeId,
    string MakeName,
    int? VehicleTypeId,
    string? VehicleTypeName);
