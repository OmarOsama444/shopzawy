using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Orders.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateCategorySpec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_category_spec",
                schema: "orders",
                table: "category_spec");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                schema: "orders",
                table: "category_spec",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "pk_category_spec",
                schema: "orders",
                table: "category_spec",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_category_spec_category_name",
                schema: "orders",
                table: "category_spec",
                column: "category_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_category_spec",
                schema: "orders",
                table: "category_spec");

            migrationBuilder.DropIndex(
                name: "ix_category_spec_category_name",
                schema: "orders",
                table: "category_spec");

            migrationBuilder.DropColumn(
                name: "id",
                schema: "orders",
                table: "category_spec");

            migrationBuilder.AddPrimaryKey(
                name: "pk_category_spec",
                schema: "orders",
                table: "category_spec",
                columns: new[] { "category_name", "spec_id" });
        }
    }
}
