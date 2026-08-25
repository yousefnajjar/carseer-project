using FluentValidation;

namespace CarSeer.Application.Makes.GetMakes;

public sealed class GetMakesQueryValidator : AbstractValidator<GetMakesQuery>
{
    public GetMakesQueryValidator()
    {
        RuleFor(query => query.Search)
            .MaximumLength(100)
            .When(query => query.Search is not null);
    }
}
