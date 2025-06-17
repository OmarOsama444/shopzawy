using Common.Application.Messaging;
using FluentValidation;
using Modules.Orders.Application.Repositories;

namespace Modules.Orders.Application.UseCases.Vendors.PaginateVendors;

public record PaginateVendorQuery(int PageNumber, int PageSize, string? Namefilter) : IQuery<PaginationResponse<VendorResponse>>;
