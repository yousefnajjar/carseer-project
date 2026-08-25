using CarSeer.Domain.Entities;
using MediatR;

namespace CarSeer.Application.Makes.GetMakes;

public sealed record GetMakesQuery(string? Search) : IRequest<IReadOnlyList<Make>>;
