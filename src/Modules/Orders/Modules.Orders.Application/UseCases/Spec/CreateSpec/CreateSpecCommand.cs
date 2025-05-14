
using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases;

public record CreateSpecCommand(IDictionary<Language, string> specNames, string dataType) : ICommand<Guid>;
public sealed class CreateSpecCommandHandler(
    ISpecRepository specRepository,
    ISpecTranslationRepository specTranslationRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateSpecCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateSpecCommand request, CancellationToken cancellationToken)
    {
        var spec = Specification.Create(request.dataType);
        specRepository.Add(spec);
        foreach (var specName in request.specNames)
        {
            specTranslationRepository.Add(
                SpecificationTranslation.Create(spec.Id, specName.Key, specName.Value)
            );
        }
        await unitOfWork.SaveChangesAsync();
        return spec.Id;
    }
}


internal class CreateSpecCommandValidator : AbstractValidator<CreateSpecCommand>
{
    public CreateSpecCommandValidator()
    {
        RuleFor(c => c.dataType).NotEmpty().Must(SpecDataType.ValidKey).WithMessage("invalid datatype");
        RuleForEach(c => c.specNames)
            .NotEmpty()
            .SetValidator(new SpecNameValidator());
    }
}

internal class SpecNameValidator : AbstractValidator<KeyValuePair<Language, string>>
{
    public SpecNameValidator()
    {
        RuleFor(x => x.Value).NotEmpty();
        RuleFor(x => x.Key).NotEmpty();
    }
}
