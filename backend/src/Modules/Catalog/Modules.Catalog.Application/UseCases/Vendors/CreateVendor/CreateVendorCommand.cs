using Common.Application.Messaging;

namespace Modules.Catalog.Application.UseCases.Vendors.CreateVendor;

public record CreateVendorCommand(
    string VendorName,
    string Email,
    string PhoneNumber,
    string Address,
    string LogoUrl,
    string ShippingZoneName,
    string Description,
    bool? Active,
    string CountryCode) : ICommand<Guid>;
