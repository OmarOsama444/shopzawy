using Common.Application.Messaging;
namespace Modules.Orders.Application.UseCases.Vendors.UpdateVendor;

public record UpdateVendorCommand(Guid Id, string? VendorName, string? Description, string? Email, string? PhoneNumber, string? Address, string? LogoUrl, string? ShipingZoneName, bool? Active, string CountryCode) : ICommand;
