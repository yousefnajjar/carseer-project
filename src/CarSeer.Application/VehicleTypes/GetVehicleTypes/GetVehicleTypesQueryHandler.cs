using CarSeer.Application.Interfaces;
using CarSeer.Domain.Entities;
using MediatR;

namespace CarSeer.Application.VehicleTypes.GetVehicleTypes;

public sealed class GetVehicleTypesQueryHandler(IVehicleCatalog catalog): IRequestHandler<GetVehicleTypesQuery, IReadOnlyList<VehicleType>>
{
    public Task<IReadOnlyList<VehicleType>> Handle(GetVehicleTypesQuery request,CancellationToken cancellationToken)
        => catalog.GetVehicleTypesAsync(request.MakeId, cancellationToken);
}
