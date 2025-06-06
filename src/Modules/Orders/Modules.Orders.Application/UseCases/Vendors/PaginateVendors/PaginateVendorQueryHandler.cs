using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.Vendors.PaginateVendors;
public sealed class PaginateVendorQueryHandler(
    IVendorRepository vendorRepository) : IQueryHandler<PaginateVendorQuery, PaginationResponse<VendorResponse>>
{
    public async Task<Result<PaginationResponse<VendorResponse>>> Handle(PaginateVendorQuery request, CancellationToken cancellationToken)
    {
        ICollection<VendorResponse> vendorResponses = await vendorRepository.Paginate(request.PageNumber, request.PageSize, request.Namefilter);
        int totalCount = await vendorRepository.TotalVendors(request.Namefilter);
        return new PaginationResponse<VendorResponse>(vendorResponses, totalCount, request.PageSize, request.PageNumber);
    }
}
