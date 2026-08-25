using CarSeer.Application.Interfaces;
using CarSeer.Domain.Entities;
using MediatR;

namespace CarSeer.Application.Models.GetModels;

public sealed class GetModelsQueryHandler(IVehicleCatalog catalog): IRequestHandler<GetModelsQuery, IReadOnlyList<VehicleModel>>
{
    public Task<IReadOnlyList<VehicleModel>> Handle(GetModelsQuery request,CancellationToken cancellationToken)
        => catalog.GetModelsAsync(request.MakeId, request.Year, request.VehicleType, cancellationToken);
}
