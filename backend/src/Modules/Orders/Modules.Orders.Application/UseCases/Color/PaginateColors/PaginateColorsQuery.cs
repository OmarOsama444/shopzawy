using Common.Application.Messaging;
using Common.Domain;
using FluentValidation;
using Modules.Orders.Application.Repositories;

namespace Modules.Orders.Application.UseCases.GetColors;

public record PaginateColorsQuery(int pageNumber = 1, int pageSize = 50, string? colorName = null) : ICommand<PaginationResponse<ColorResponse>>;
public sealed class GetColorsQueryHandler(IColorRepository colorRepository) : ICommandHandler<PaginateColorsQuery, PaginationResponse<ColorResponse>>
{
    public async Task<Result<PaginationResponse<ColorResponse>>> Handle(PaginateColorsQuery request, CancellationToken cancellationToken)
    {
        var colors = await colorRepository.Paginate(request.pageSize, request.pageNumber, request.colorName);
        var colorsCount = await colorRepository.TotalColors(request.colorName);
        return new PaginationResponse<ColorResponse>(colors, colorsCount, request.pageSize, request.pageNumber);
    }
}
internal class GetColorsQueryValidator : AbstractValidator<PaginateColorsQuery>
{
    public GetColorsQueryValidator()
    {
        RuleFor(x => x.pageNumber).NotEmpty().GreaterThan(0);
        RuleFor(x => x.pageSize).NotEmpty().InclusiveBetween(1, 50);
    }
}