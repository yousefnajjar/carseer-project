using CarSeer.Domain.Entities;
using MediatR;

namespace CarSeer.Application.Models.GetModels;

public sealed record GetModelsQuery(int MakeId, int Year, string? VehicleType): IRequest<IReadOnlyList<VehicleModel>>;
