using Common.Domain;
using Modules.Catalog.Domain.Entities;

namespace Modules.Catalog.Application.Repositories;

public interface IVendorRepository : IRepository<Vendor>
{
    public Task<ICollection<VendorResponse>> Paginate(int pageNumber, int pageSize, string? namefiler);
    public Task<Vendor?> GetVendorByEmail(string Email);
    public Task<Vendor?> GetVendorByPhone(string Phone);
    public Task<int> TotalVendors(string? namefiler);
}

public class VendorResponse
{
    public Guid Id { get; set; }
    public string VendorName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public string ShippingZoneName { get; set; } = string.Empty;
    public int ProductsNumber { get; set; }
    public bool Active { get; set; }
    public VendorResponse()
    {
    }
    public VendorResponse(
        Guid id,
        string vendorName,
        string description,
        string email,
        string phoneNumber,
        string address,
        string logoUrl,
        string shippingZoneName,
        bool active,
        int productsNumber)
    {
        Id = id;
        VendorName = vendorName;
        Description = description;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
        LogoUrl = logoUrl;
        ShippingZoneName = shippingZoneName;
        ProductsNumber = productsNumber;
        Active = active;
    }
}
