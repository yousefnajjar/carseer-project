using FluentValidation;

namespace CarSeer.Application.VehicleTypes.GetVehicleTypes;

public sealed class GetVehicleTypesQueryValidator : AbstractValidator<GetVehicleTypesQuery>
{
    public GetVehicleTypesQueryValidator()
    {
        RuleFor(query => query.MakeId).GreaterThan(0);
    }
}
