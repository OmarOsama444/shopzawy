using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Users.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TrackLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "last_login_date",
                schema: "users",
                table: "user",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_login_date",
                schema: "users",
                table: "user");
        }
    }
}
