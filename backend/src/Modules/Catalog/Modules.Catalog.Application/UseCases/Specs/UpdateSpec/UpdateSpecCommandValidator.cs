using FluentValidation;

namespace Modules.Catalog.Application.UseCases.Specs.UpdateSpec;

internal class UpdateSpecCommandValidator : AbstractValidator<UpdateSpecCommand>
{
    public UpdateSpecCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}