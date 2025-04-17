using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Orders.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class all : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_product_price",
                schema: "orders",
                table: "product");

            migrationBuilder.DropColumn(
                name: "price",
                schema: "orders",
                table: "product");

            migrationBuilder.RenameColumn(
                name: "l_description",
                schema: "orders",
                table: "product",
                newName: "short_description");

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "orders",
                table: "product",
                newName: "long_description");

            migrationBuilder.AddColumn<bool>(
                name: "in_stock",
                schema: "orders",
                table: "product",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "in_stock",
                schema: "orders",
                table: "product");

            migrationBuilder.RenameColumn(
                name: "short_description",
                schema: "orders",
                table: "product",
                newName: "l_description");

            migrationBuilder.RenameColumn(
                name: "long_description",
                schema: "orders",
                table: "product",
                newName: "description");

            migrationBuilder.AddColumn<float>(
                name: "price",
                schema: "orders",
                table: "product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "ix_product_price",
                schema: "orders",
                table: "product",
                column: "price");
        }
    }
}
