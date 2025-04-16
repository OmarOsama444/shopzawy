using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Orders.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCategoryVariations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_specification_value",
                schema: "orders");

            migrationBuilder.DropColumn(
                name: "stock_keeping_unit",
                schema: "orders",
                table: "product");

            migrationBuilder.CreateTable(
                name: "category_specification_option",
                schema: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_specification_id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category_specification_option", x => x.id);
                    table.ForeignKey(
                        name: "fk_category_specification_option_category_specification_catego",
                        column: x => x.category_specification_id,
                        principalSchema: "orders",
                        principalTable: "category_specification",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_item",
                schema: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    stock_keeping_unit = table.Column<string>(type: "text", nullable: false),
                    quantity_in_stock = table.Column<int>(type: "integer", nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<float>(type: "real", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_item_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "orders",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_item_options",
                schema: "orders",
                columns: table => new
                {
                    product_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_specification_option_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_item_options", x => new { x.product_item_id, x.category_specification_option_id });
                    table.ForeignKey(
                        name: "fk_product_item_options_category_specification_option_category",
                        column: x => x.category_specification_option_id,
                        principalSchema: "orders",
                        principalTable: "category_specification_option",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_item_options_product_item_product_item_id",
                        column: x => x.product_item_id,
                        principalSchema: "orders",
                        principalTable: "product_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_category_specification_option_category_specification_id",
                schema: "orders",
                table: "category_specification_option",
                column: "category_specification_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_item_product_id",
                schema: "orders",
                table: "product_item",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_item_options_category_specification_option_id",
                schema: "orders",
                table: "product_item_options",
                column: "category_specification_option_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_item_options",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "category_specification_option",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "product_item",
                schema: "orders");

            migrationBuilder.AddColumn<string>(
                name: "stock_keeping_unit",
                schema: "orders",
                table: "product",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "product_specification_value",
                schema: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_spec_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_specification_value", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_specification_value_category_specification_category",
                        column: x => x.category_spec_id,
                        principalSchema: "orders",
                        principalTable: "category_specification",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_specification_value_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "orders",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_product_specification_value_category_spec_id",
                schema: "orders",
                table: "product_specification_value",
                column: "category_spec_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_specification_value_product_id",
                schema: "orders",
                table: "product_specification_value",
                column: "product_id");
        }
    }
}
