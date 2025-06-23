using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Modules.Users.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateadminroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "users",
                table: "role_permission",
                columns: new[] { "permission_id", "role_id" },
                values: new object[,]
                {
                    { "permission:create", "Admin" },
                    { "permission:read", "Admin" },
                    { "permission:update", "Admin" },
                    { "role:create", "Admin" },
                    { "role:permission:update", "Admin" },
                    { "role:read", "Admin" },
                    { "user:create", "Admin" },
                    { "user:role:update", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                keyValues: new object[] { "user:create", "Admin" });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permission",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { "user:role:update", "Admin" });
        }
    }
}
