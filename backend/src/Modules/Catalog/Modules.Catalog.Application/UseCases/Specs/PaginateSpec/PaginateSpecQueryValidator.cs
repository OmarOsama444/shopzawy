using FluentValidation;

namespace Modules.Catalog.Application.UseCases.Specs.PaginateSpec;

public class PaginateSpecQueryValidator : AbstractValidator<PaginateSpecQuery>
{
    public PaginateSpecQueryValidator()
    {
        RuleFor(p => p.PageNumber).GreaterThan(0);
        RuleFor(p => p.PageSize).GreaterThan(0).LessThan(51);
    }
}
