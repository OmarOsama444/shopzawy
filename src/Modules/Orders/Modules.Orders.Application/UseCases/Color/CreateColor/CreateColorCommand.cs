using System.Security.Permissions;
using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Common.Domain.Exceptions;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Application.UseCases.CreateColor;

public record CreateColorCommand(string name, string code) : ICommand;
public sealed class CreateColorCommandHandler(IColorRepository colorRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreateColorCommand>
{
    public async Task<Result> Handle(CreateColorCommand request, CancellationToken cancellationToken)
    {
        Color? color = await colorRepository.GetByIdAsync(request.code);
        if (color is not null)
            return new ConflictException("Color.Conflict.Code", $"color with code {request.code} already exists");
        color = await colorRepository.GetByName(request.name);
        if (color is not null)
            return new ConflictException("Color.Conflict.Name", $"color with name {request.name} already exists");
        colorRepository.Add(Color.Create(request.code, request.name));
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}

internal class CreateColorCommandValidator : AbstractValidator<CreateColorCommand>
{
    public CreateColorCommandValidator()
    {
        RuleFor(c => c.code)
        .NotEmpty()
        .Matches(@"^#?[0-9A-Fa-f]{6}$")
        .WithMessage("Code must be a valid 6-digit hexadecimal color code.");
        RuleFor(c => c.name).NotEmpty().MinimumLength(3).MaximumLength(30);
    }
}