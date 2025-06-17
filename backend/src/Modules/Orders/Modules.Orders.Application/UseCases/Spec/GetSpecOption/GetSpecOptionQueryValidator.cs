using FluentValidation;

namespace Modules.Orders.Application.UseCases.Spec.GetSpecOption;

public class GetSpecOptionQueryValidator : AbstractValidator<GetSpecOptionQuery>
{
    public GetSpecOptionQueryValidator()
    {
        RuleFor(x => x.SpecId).NotEmpty();
    }
}
