using System.Data.Common;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Modules.Common.Infrastructure;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Infrastructure.Data;
using Modules.Users.Infrastructure.Repositories;

namespace Modules.Orders.Infrastructure.Repositories;

public class VendorRepository(OrdersDbContext ordersDbContext, IDbConnectionFactory dbConnectionFactory) : Repository<Vendor, OrdersDbContext>(ordersDbContext), IVendorRepository
{
    public async Task<Vendor?> GetVendorByEmail(string Email)
    {
        return await context.Vendors.FirstOrDefaultAsync(v => v.Email == Email);
    }

    public async Task<Vendor?> GetVendorByPhone(string Phone)
    {
        return await context.Vendors.FirstOrDefaultAsync(v => v.PhoneNumber == Phone);
    }

    public async Task<ICollection<VendorResponse>> Paginate(int pageNumber, int pageSize, string? namefilter)
    {
        await using DbConnection sqlConnection = await dbConnectionFactory.CreateSqlConnection();

        int offset = (pageNumber - 1) * pageSize;

        string query = $"""
        SELECT 
            V.Id as {nameof(VendorResponse.Id)},
            V.Vendor_Name as {nameof(VendorResponse.VendorName)},
            V.Description as {nameof(VendorResponse.Description)},
            V.Email as {nameof(VendorResponse.Email)},
            V.Phone_Number as {nameof(VendorResponse.PhoneNumber)},
            V.Address as {nameof(VendorResponse.Address)},
            V.Logo_Url as {nameof(VendorResponse.LogoUrl)},
            v.Shiping_Zone_Name as {nameof(VendorResponse.ShippingZoneName)},
            v.Active as {nameof(VendorResponse.Active)},
            COUNT(P.Id) AS {nameof(VendorResponse.ProductsNumber)}
        FROM 
            Orders.Vendor AS V
        LEFT JOIN 
            Orders.Product AS P 
        ON 
            P.Vendor_Id = V.Id
        WHERE 
            (@namefilter IS NULL OR V.Vendor_Name ILIKE @namefilter || '%')
        GROUP BY 
            V.Id, V.Vendor_Name, V.Description, V.Email, V.Phone_Number, V.Address, V.Logo_Url, v.Shiping_Zone_Name
        ORDER BY 
            V.Vendor_Name
        LIMIT 
            @pageSize 
        OFFSET 
            @offset;
    """;

        var vendors = await sqlConnection.QueryAsync<VendorResponse>(query, new
        {
            namefilter,
            pageSize,
            offset
        });

        return vendors.ToList();
    }

    public async Task<int> TotalVendors(string? namefilter)
    {
        await using DbConnection sqlConnection = await dbConnectionFactory.CreateSqlConnection();
        string Query =
        $"""
        SELECT
        COUNT(*)
        FROM 
        {Schemas.Orders}.Vendor as V
        WHERE 
        (@namefilter IS NULL OR V.Vendor_Name ILIKE @namefilter || '%')
        """;
        return await sqlConnection.ExecuteScalarAsync<int>(Query, new { namefilter });
    }
}