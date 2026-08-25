using CarSeer.Domain.Entities;

namespace CarSeer.Application.Interfaces;

public interface IVehicleCatalog
{
    Task<IReadOnlyList<Make>> GetMakesAsync(CancellationToken cancellationToken);

    Task<IReadOnlyList<VehicleType>> GetVehicleTypesAsync(int makeId, CancellationToken cancellationToken);

    Task<IReadOnlyList<VehicleModel>> GetModelsAsync(int makeId,int year,string? vehicleType,CancellationToken cancellationToken);
}
