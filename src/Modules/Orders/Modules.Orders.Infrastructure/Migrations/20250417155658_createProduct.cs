using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Orders.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class createProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_on",
                schema: "orders",
                table: "vendor",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                schema: "orders",
                table: "product",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_on",
                schema: "orders",
                table: "product",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "dimension_unit",
                schema: "orders",
                table: "product",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "height",
                schema: "orders",
                table: "product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "image_url",
                schema: "orders",
                table: "product",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "length",
                schema: "orders",
                table: "product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "price",
                schema: "orders",
                table: "product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "weight",
                schema: "orders",
                table: "product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "weight_unit",
                schema: "orders",
                table: "product",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "width",
                schema: "orders",
                table: "product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_on",
                schema: "orders",
                table: "color",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "created_on",
                schema: "orders",
                table: "category",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "created_on",
                schema: "orders",
                table: "brand",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "ix_product_created_on",
                schema: "orders",
                table: "product",
                column: "created_on");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_product_created_on",
                schema: "orders",
                table: "product");

            migrationBuilder.DropColumn(
                name: "created_on",
                schema: "orders",
                table: "vendor");

            migrationBuilder.DropColumn(
                name: "Tags",
                schema: "orders",
                table: "product");

            migrationBuilder.DropColumn(
                name: "created_on",
                schema: "orders",
                table: "product");

            migrationBuilder.DropColumn(
                name: "dimension_unit",
                schema: "orders",
                table: "product");

            migrationBuilder.DropColumn(
                name: "height",
                schema: "orders",
                table: "product");

            migrationBuilder.DropColumn(
                name: "image_url",
                schema: "orders",
                table: "product");

            migrationBuilder.DropColumn(
                name: "length",
                schema: "orders",
                table: "product");

            migrationBuilder.DropColumn(
                name: "price",
                schema: "orders",
                table: "product");

            migrationBuilder.DropColumn(
                name: "weight",
                schema: "orders",
                table: "product");

            migrationBuilder.DropColumn(
                name: "weight_unit",
                schema: "orders",
                table: "product");

            migrationBuilder.DropColumn(
                name: "width",
                schema: "orders",
                table: "product");

            migrationBuilder.DropColumn(
                name: "created_on",
                schema: "orders",
                table: "color");

            migrationBuilder.DropColumn(
                name: "created_on",
                schema: "orders",
                table: "category");

            migrationBuilder.DropColumn(
                name: "created_on",
                schema: "orders",
                table: "brand");
        }
    }
}
