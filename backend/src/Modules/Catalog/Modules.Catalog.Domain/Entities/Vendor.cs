using Common.Domain.Entities;

namespace Modules.Catalog.Domain.Entities
{
    public class Vendor : Entity
    {
        public Guid Id { get; set; }
        public string VendorName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public string ShipingZoneName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public virtual ICollection<Product> Products { get; set; } = [];
        public bool Active { get; set; }
        public static Vendor Create(
        string vendorName,
        string email,
        string phoneNumber,
        string address,
        string logoUrl,
        string shippingZoneName,
        string description,
        bool? active)
        {
            return new Vendor
            {
                Id = Guid.NewGuid(),
                VendorName = vendorName,
                Email = email,
                PhoneNumber = phoneNumber,
                Address = address,
                LogoUrl = logoUrl,
                ShipingZoneName = shippingZoneName,
                Description = description,
                Active = active ?? true,
                CreatedOn = DateTime.UtcNow
            };
        }
        public void Update(
        string? vendorName,
        string? description,
        string? email,
        string? phoneNumber,
        string? address,
        string? logoUrl,
        string? shipingZoneName,
        bool? active)
        {
            if (!string.IsNullOrWhiteSpace(vendorName))
                VendorName = vendorName;

            if (!string.IsNullOrWhiteSpace(description))
                Description = description;

            if (!string.IsNullOrWhiteSpace(email))
                Email = email;

            if (!string.IsNullOrWhiteSpace(phoneNumber))
                PhoneNumber = phoneNumber;

            if (!string.IsNullOrWhiteSpace(address))
                Address = address;

            if (!string.IsNullOrWhiteSpace(logoUrl))
                LogoUrl = logoUrl;

            if (!string.IsNullOrWhiteSpace(shipingZoneName))
                ShipingZoneName = shipingZoneName;
            if (active.HasValue)
                this.Active = active.Value;
        }

    }
}
