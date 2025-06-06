using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.Vendors.PaginateVendors;
internal class PaginationVendorqueryValidator : AbstractValidator<PaginateVendorQuery>
{
    public PaginationVendorqueryValidator()
    {
        RuleFor(v => v.PageNumber).NotEmpty().GreaterThan(0);
        RuleFor(v => v.PageSize).NotEmpty().GreaterThan(0).LessThan(50);
    }
}