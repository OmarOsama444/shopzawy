using Common.Application.Messaging;

namespace Modules.Orders.Application.UseCases.CreateVendor;

public record CreateVendorCommand(
    string vendorName,
    string email,
    string phoneNumber,
    string address,
    string logoUrl,
    string shippingZoneName,
    string description,
    bool? active,
    string countryCode) : ICommand<Guid>;
