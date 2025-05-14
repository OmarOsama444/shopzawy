using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.Spec.PaginateSpec;

public record PaginateSpecQuery(int pageNumber, int pageSize, string? name, Language lang_code) : IQuery<PaginationResponse<SpecResponse>>;

public class PaginateSpecQueryHandler(ISpecRepository specRepository) : IQueryHandler<PaginateSpecQuery, PaginationResponse<SpecResponse>>
{
    public async Task<Result<PaginationResponse<SpecResponse>>> Handle(PaginateSpecQuery request, CancellationToken cancellationToken)
    {
        var specs = await specRepository.Paginate(request.pageNumber, request.pageSize, request.name, request.lang_code);
        int total = await specRepository.Total(request.name, request.lang_code);
        return new PaginationResponse<SpecResponse>(specs, total, request.pageSize, request.pageNumber);
    }
}

public class PaginateSpecQueryValidator : AbstractValidator<PaginateSpecQuery>
{
    public PaginateSpecQueryValidator()
    {
        RuleFor(p => p.pageNumber).GreaterThan(0);
        RuleFor(p => p.pageSize).GreaterThan(0).LessThan(51);
    }
}
