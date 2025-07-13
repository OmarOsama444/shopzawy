using Common.Application.Messaging;
using FluentValidation;
using Modules.Catalog.Application.Repositories;

namespace Modules.Catalog.Application.UseCases.Vendors.PaginateVendors;

public record PaginateVendorQuery(int PageNumber, int PageSize, string? Namefilter) : IQuery<PaginationResponse<VendorResponse>>;
