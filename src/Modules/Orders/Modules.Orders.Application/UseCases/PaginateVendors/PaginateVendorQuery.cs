using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.PaginateVendors;

public record PaginateVendorQuery(int pageNumber, int pageSize, string? namefilter) : IQuery<PaginationResponse<VendorResponse>>;
public sealed class PaginateVendorQueryHandler(
    IVendorRepository vendorRepository) : IQueryHandler<PaginateVendorQuery, PaginationResponse<VendorResponse>>
{
    public async Task<Result<PaginationResponse<VendorResponse>>> Handle(PaginateVendorQuery request, CancellationToken cancellationToken)
    {
        ICollection<VendorResponse> vendorResponses = await vendorRepository.Paginate(request.pageNumber, request.pageSize, request.namefilter);
        int totalCount = await vendorRepository.TotalVendors(request.namefilter);
        return new PaginationResponse<VendorResponse>(vendorResponses, totalCount, request.pageSize, request.pageNumber);
    }
}
internal class PaginationVendorqueryValidator : AbstractValidator<PaginateVendorQuery>
{
    public PaginationVendorqueryValidator()
    {
        RuleFor(v => v.pageNumber).NotEmpty().GreaterThan(0);
        RuleFor(v => v.pageSize).NotEmpty().GreaterThan(0).LessThan(50);
    }
}