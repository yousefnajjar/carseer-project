using FluentValidation;

namespace CarSeer.Application.Models.GetModels;

public sealed class GetModelsQueryValidator : AbstractValidator<GetModelsQuery>
{
    public GetModelsQueryValidator()
    {
        RuleFor(query => query.MakeId).GreaterThan(0);

        RuleFor(query => query.Year).InclusiveBetween(1920, DateTime.UtcNow.Year + 1);

        RuleFor(query => query.VehicleType).MaximumLength(100).When(query => query.VehicleType is not null);
    }
}
