using CarSeer.Application.Interfaces;
using CarSeer.Domain.Entities;
using MediatR;

namespace CarSeer.Application.Makes.GetMakes;

public sealed class GetMakesQueryHandler(IVehicleCatalog catalog): IRequestHandler<GetMakesQuery, IReadOnlyList<Make>>
{
    private const int MaxResults = 100;

    public async Task<IReadOnlyList<Make>> Handle(GetMakesQuery request, CancellationToken cancellationToken)
    {
        var makes = await catalog.GetMakesAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(request.Search))
        {
            return makes.Take(MaxResults).ToList();
        }

        var term = request.Search.Trim();

        return makes
            .Where(make => make.Name.Contains(term, StringComparison.OrdinalIgnoreCase))
            .OrderBy(make => make.Name.StartsWith(term, StringComparison.OrdinalIgnoreCase) ? 0 : 1)
            .ThenBy(make => make.Name)
            .Take(MaxResults)
            .ToList();
    }
}
