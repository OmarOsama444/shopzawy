using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Orders.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "active",
                schema: "orders",
                table: "product");

            migrationBuilder.DropColumn(
                name: "featured",
                schema: "orders",
                table: "product");

            migrationBuilder.DropColumn(
                name: "new_arrival",
                schema: "orders",
                table: "product");

            migrationBuilder.RenameColumn(
                name: "image_url",
                schema: "orders",
                table: "product_item",
                newName: "image_urls");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_on",
                schema: "orders",
                table: "product_item",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "ix_product_item_created_on",
                schema: "orders",
                table: "product_item",
                column: "created_on");

            migrationBuilder.CreateIndex(
                name: "ix_product_item_stock_keeping_unit",
                schema: "orders",
                table: "product_item",
                column: "stock_keeping_unit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_product_item_created_on",
                schema: "orders",
                table: "product_item");

            migrationBuilder.DropIndex(
                name: "ix_product_item_stock_keeping_unit",
                schema: "orders",
                table: "product_item");

            migrationBuilder.DropColumn(
                name: "created_on",
                schema: "orders",
                table: "product_item");

            migrationBuilder.RenameColumn(
                name: "image_urls",
                schema: "orders",
                table: "product_item",
                newName: "image_url");

            migrationBuilder.AddColumn<bool>(
                name: "active",
                schema: "orders",
                table: "product",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "featured",
                schema: "orders",
                table: "product",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "new_arrival",
                schema: "orders",
                table: "product",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
