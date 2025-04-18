
using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Common.Domain.Exceptions;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases;

public record CreateSpecCommand(string specName, string dataType) : ICommand<Guid>;

public sealed class CreateSpecCommandHandler(
    ISpecRepository specRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateSpecCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateSpecCommand request, CancellationToken cancellationToken)
    {
        var spec = Specification.Create(request.specName, request.dataType);
        specRepository.Add(spec);
        await unitOfWork.SaveChangesAsync();
        return spec.Id;
    }
}


internal class CreateSpecCommandValidator : AbstractValidator<CreateSpecCommand>
{
    public CreateSpecCommandValidator()
    {
        RuleFor(c => c.specName).NotEmpty();
        RuleFor(c => c.dataType).NotEmpty().Must(SpecDataType.ValidKey).WithMessage("invalid datatype");
    }
}
