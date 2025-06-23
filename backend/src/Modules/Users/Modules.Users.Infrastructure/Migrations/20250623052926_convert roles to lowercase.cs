using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Modules.Users.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class convertrolestolowercase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "banner:create", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "banner:delete", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "banner:read", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "brand:create", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "brand:read", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "brand:update", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "category:create", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "category:delete", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "category:read", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "category:update", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "color:create", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "color:read", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "permission:create", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "permission:read", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "permission:update", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "product:create", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "product:item:create", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "product:item:delete", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "product:item:read", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "role:create", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "role:permission:update", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "role:read", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "spec:create", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "spec:read", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "spec:update", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "user:create", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "user:role:update", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "vendor:create", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "vendor:read", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "vendor:update", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "banner:read", "Default" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "brand:read", "Default" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "category:read", "Default" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "color:read", "Default" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "product:item:read", "Default" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "spec:read", "Default" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "vendor:read", "Default" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "auth:login", "Guest" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "banner:read", "Guest" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "brand:read", "Guest" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "category:read", "Guest" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "product:item:read", "Guest" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "user:create", "Guest" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "user_role",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { "Admin", new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "roles",
                keyColumn: "name",
                keyValue: "Admin");

            migrationBuilder.DeleteData(
                schema: "users",
                table: "roles",
                keyColumn: "name",
                keyValue: "Default");

            migrationBuilder.DeleteData(
                schema: "users",
                table: "roles",
                keyColumn: "name",
                keyValue: "Guest");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "auth:login",
                column: "module",
                value: "users");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "banner:create",
                column: "module",
                value: "orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "banner:delete",
                column: "module",
                value: "orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "banner:read",
                column: "module",
                value: "orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "brand:create",
                column: "module",
                value: "orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "brand:read",
                column: "module",
                value: "orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "brand:update",
                column: "module",
                value: "orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "category:create",
                column: "module",
                value: "orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "category:delete",
                column: "module",
                value: "orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "category:read",
                column: "module",
                value: "orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "category:update",
                column: "module",
                value: "orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "color:create",
                column: "module",
                value: "orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "color:read",
                column: "module",
                value: "orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "permission:create",
                column: "module",
                value: "users");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "permission:read",
                column: "module",
                value: "users");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "permission:update",
                column: "module",
                value: "users");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "product:create",
                column: "module",
                value: "orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "product:item:create",
                column: "module",
                value: "orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "product:item:delete",
                column: "module",
                value: "orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "product:item:read",
                column: "module",
                value: "orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "role:create",
                column: "module",
                value: "users");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "role:permission:update",
                column: "module",
                value: "users");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "role:read",
                column: "module",
                value: "users");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "spec:create",
                column: "module",
                value: "orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "spec:read",
                column: "module",
                value: "orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "spec:update",
                column: "module",
                value: "orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "user:create",
                column: "module",
                value: "users");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "user:role:update",
                column: "module",
                value: "users");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "vendor:create",
                column: "module",
                value: "orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "vendor:read",
                column: "module",
                value: "orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "vendor:update",
                column: "module",
                value: "orders");

            migrationBuilder.InsertData(
                schema: "users",
                table: "roles",
                columns: new[] { "name", "created_on_utc" },
                values: new object[,]
                {
                    { "admin", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "default", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "guest", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "role_permission",
                columns: new[] { "permission_id", "role_id" },
                values: new object[,]
                {
                    { "banner:create", "admin" },
                    { "banner:delete", "admin" },
                    { "banner:read", "admin" },
                    { "brand:create", "admin" },
                    { "brand:read", "admin" },
                    { "brand:update", "admin" },
                    { "category:create", "admin" },
                    { "category:delete", "admin" },
                    { "category:read", "admin" },
                    { "category:update", "admin" },
                    { "color:create", "admin" },
                    { "color:read", "admin" },
                    { "permission:create", "admin" },
                    { "permission:read", "admin" },
                    { "permission:update", "admin" },
                    { "product:create", "admin" },
                    { "product:item:create", "admin" },
                    { "product:item:delete", "admin" },
                    { "product:item:read", "admin" },
                    { "role:create", "admin" },
                    { "role:permission:update", "admin" },
                    { "role:read", "admin" },
                    { "spec:create", "admin" },
                    { "spec:read", "admin" },
                    { "spec:update", "admin" },
                    { "user:create", "admin" },
                    { "user:role:update", "admin" },
                    { "vendor:create", "admin" },
                    { "vendor:read", "admin" },
                    { "vendor:update", "admin" },
                    { "banner:read", "default" },
                    { "brand:read", "default" },
                    { "category:read", "default" },
                    { "color:read", "default" },
                    { "product:item:read", "default" },
                    { "spec:read", "default" },
                    { "vendor:read", "default" },
                    { "auth:login", "guest" },
                    { "banner:read", "guest" },
                    { "brand:read", "guest" },
                    { "category:read", "guest" },
                    { "product:item:read", "guest" },
                    { "user:create", "guest" }
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "user_role",
                columns: new[] { "role_id", "user_id" },
                values: new object[] { "admin", new Guid("11111111-1111-1111-1111-111111111111") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "banner:create", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "banner:delete", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "banner:read", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "brand:create", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "brand:read", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "brand:update", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "category:create", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "category:delete", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "category:read", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "category:update", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "color:create", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "color:read", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "permission:create", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "permission:read", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "permission:update", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "product:create", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "product:item:create", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "product:item:delete", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "product:item:read", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "role:create", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "role:permission:update", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "role:read", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "spec:create", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "spec:read", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "spec:update", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "user:create", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "user:role:update", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "vendor:create", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "vendor:read", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "vendor:update", "admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "banner:read", "default" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "brand:read", "default" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "category:read", "default" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "color:read", "default" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "product:item:read", "default" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "spec:read", "default" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "vendor:read", "default" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "auth:login", "guest" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "banner:read", "guest" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "brand:read", "guest" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "category:read", "guest" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "product:item:read", "guest" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "user:create", "guest" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "user_role",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { "admin", new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "roles",
                keyColumn: "name",
                keyValue: "admin");

            migrationBuilder.DeleteData(
                schema: "users",
                table: "roles",
                keyColumn: "name",
                keyValue: "default");

            migrationBuilder.DeleteData(
                schema: "users",
                table: "roles",
                keyColumn: "name",
                keyValue: "guest");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "auth:login",
                column: "module",
                value: "Users");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "banner:create",
                column: "module",
                value: "Orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "banner:delete",
                column: "module",
                value: "Orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "banner:read",
                column: "module",
                value: "Orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "brand:create",
                column: "module",
                value: "Orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "brand:read",
                column: "module",
                value: "Orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "brand:update",
                column: "module",
                value: "Orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "category:create",
                column: "module",
                value: "Orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "category:delete",
                column: "module",
                value: "Orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "category:read",
                column: "module",
                value: "Orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "category:update",
                column: "module",
                value: "Orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "color:create",
                column: "module",
                value: "Orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "color:read",
                column: "module",
                value: "Orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "permission:create",
                column: "module",
                value: "Users");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "permission:read",
                column: "module",
                value: "Users");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "permission:update",
                column: "module",
                value: "Users");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "product:create",
                column: "module",
                value: "Orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "product:item:create",
                column: "module",
                value: "Orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "product:item:delete",
                column: "module",
                value: "Orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "product:item:read",
                column: "module",
                value: "Orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "role:create",
                column: "module",
                value: "Users");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "role:permission:update",
                column: "module",
                value: "Users");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "role:read",
                column: "module",
                value: "Users");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "spec:create",
                column: "module",
                value: "Orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "spec:read",
                column: "module",
                value: "Orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "spec:update",
                column: "module",
                value: "Orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "user:create",
                column: "module",
                value: "Users");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "user:role:update",
                column: "module",
                value: "Users");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "vendor:create",
                column: "module",
                value: "Orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "vendor:read",
                column: "module",
                value: "Orders");

            migrationBuilder.UpdateData(
                schema: "users",
                table: "permission",
                keyColumn: "name",
                keyValue: "vendor:update",
                column: "module",
                value: "Orders");

            migrationBuilder.InsertData(
                schema: "users",
                table: "roles",
                columns: new[] { "name", "created_on_utc" },
                values: new object[,]
                {
                    { "Admin", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "Default", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "Guest", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "role_permission",
                columns: new[] { "permission_id", "role_id" },
                values: new object[,]
                {
                    { "banner:create", "Admin" },
                    { "banner:delete", "Admin" },
                    { "banner:read", "Admin" },
                    { "brand:create", "Admin" },
                    { "brand:read", "Admin" },
                    { "brand:update", "Admin" },
                    { "category:create", "Admin" },
                    { "category:delete", "Admin" },
                    { "category:read", "Admin" },
                    { "category:update", "Admin" },
                    { "color:create", "Admin" },
                    { "color:read", "Admin" },
                    { "permission:create", "Admin" },
                    { "permission:read", "Admin" },
                    { "permission:update", "Admin" },
                    { "product:create", "Admin" },
                    { "product:item:create", "Admin" },
                    { "product:item:delete", "Admin" },
                    { "product:item:read", "Admin" },
                    { "role:create", "Admin" },
                    { "role:permission:update", "Admin" },
                    { "role:read", "Admin" },
                    { "spec:create", "Admin" },
                    { "spec:read", "Admin" },
                    { "spec:update", "Admin" },
                    { "user:create", "Admin" },
                    { "user:role:update", "Admin" },
                    { "vendor:create", "Admin" },
                    { "vendor:read", "Admin" },
                    { "vendor:update", "Admin" },
                    { "banner:read", "Default" },
                    { "brand:read", "Default" },
                    { "category:read", "Default" },
                    { "color:read", "Default" },
                    { "product:item:read", "Default" },
                    { "spec:read", "Default" },
                    { "vendor:read", "Default" },
                    { "auth:login", "Guest" },
                    { "banner:read", "Guest" },
                    { "brand:read", "Guest" },
                    { "category:read", "Guest" },
                    { "product:item:read", "Guest" },
                    { "user:create", "Guest" }
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "user_role",
                columns: new[] { "role_id", "user_id" },
                values: new object[] { "Admin", new Guid("11111111-1111-1111-1111-111111111111") });
        }
    }
}
