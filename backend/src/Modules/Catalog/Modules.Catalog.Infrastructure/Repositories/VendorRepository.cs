using System.Data.Common;
using Common.Application;
using Common.Infrastructure;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Application.Abstractions;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.Entities;
using Modules.Catalog.Infrastructure.Data;

namespace Modules.Catalog.Infrastructure.Repositories;

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
        if (!String.IsNullOrEmpty(namefilter))
            namefilter += "%";
        else
            namefilter = null;
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
            {Schemas.Catalog}.Vendor AS V
        LEFT JOIN 
            {Schemas.Catalog}.Product AS P 
        ON 
            P.Vendor_Id = V.Id
        {(string.IsNullOrWhiteSpace(namefilter) ? "" : "WHERE V.Vendor_Name ILIKE @namefilter")}
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

        return [.. vendors];
    }

    public async Task<int> TotalVendors(string? namefilter)
    {
        if (!String.IsNullOrEmpty(namefilter))
            namefilter += "%";
        else
            namefilter = null;
        await using DbConnection sqlConnection = await dbConnectionFactory.CreateSqlConnection();
        string Query =
        $"""
        SELECT
        COUNT(*)
        FROM 
        {Schemas.Catalog}.Vendor as V
        {(string.IsNullOrWhiteSpace(namefilter) ? "" : "WHERE V.Vendor_Name ILIKE @namefilter")}
        """;
        return await sqlConnection.ExecuteScalarAsync<int>(Query, new { namefilter });
    }
}