using CarSeer.Domain.Entities;
using MediatR;

namespace CarSeer.Application.VehicleTypes.GetVehicleTypes;

public sealed record GetVehicleTypesQuery(int MakeId) : IRequest<IReadOnlyList<VehicleType>>;
