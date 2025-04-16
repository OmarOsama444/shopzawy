using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.GetColors;

public record GetColorsQuery() : ICommand<ICollection<colorResponse>>;
public sealed class GetColorsQueryHandler(IColorRepository colorRepository) : ICommandHandler<GetColorsQuery, ICollection<colorResponse>>
{
    public async Task<Result<ICollection<colorResponse>>> Handle(GetColorsQuery request, CancellationToken cancellationToken)
    {
        ICollection<colorResponse> colors = (await colorRepository.GetAllAsync()).Select(c => new colorResponse(c.Code, c.Name)).ToList();
        return Result<ICollection<colorResponse>>.Success(colors);
    }
}

public record colorResponse(string code, string name);

internal class GetColorsQueryValidator : AbstractValidator<GetColorsQuery>
{
    public GetColorsQueryValidator()
    {
    }
}