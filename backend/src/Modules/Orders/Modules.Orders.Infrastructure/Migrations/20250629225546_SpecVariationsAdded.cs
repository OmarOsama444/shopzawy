using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Orders.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SpecVariationsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_product_item_options_product_item_id",
                schema: "orders",
                table: "product_item_options");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "orders",
                table: "product_item_options",
                newName: "product_item_id");

            migrationBuilder.CreateTable(
                name: "product_item_option_color",
                schema: "orders",
                columns: table => new
                {
                    product_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    specification_id = table.Column<Guid>(type: "uuid", nullable: false),
                    color_code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_item_option_color", x => new { x.product_item_id, x.specification_id, x.color_code });
                    table.ForeignKey(
                        name: "fk_product_item_option_color_color_color_code",
                        column: x => x.color_code,
                        principalSchema: "orders",
                        principalTable: "color",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_item_option_color_product_item_product_item_id",
                        column: x => x.product_item_id,
                        principalSchema: "orders",
                        principalTable: "product_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_item_option_color_specification_specification_id",
                        column: x => x.specification_id,
                        principalSchema: "orders",
                        principalTable: "specification",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_item_option_numeric",
                schema: "orders",
                columns: table => new
                {
                    product_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    specification_id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_item_option_numeric", x => new { x.product_item_id, x.specification_id, x.value });
                    table.ForeignKey(
                        name: "fk_product_item_option_numeric_product_item_product_item_id",
                        column: x => x.product_item_id,
                        principalSchema: "orders",
                        principalTable: "product_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_item_option_numeric_specification_specification_id",
                        column: x => x.specification_id,
                        principalSchema: "orders",
                        principalTable: "specification",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_product_item_option_color_color_code",
                schema: "orders",
                table: "product_item_option_color",
                column: "color_code");

            migrationBuilder.CreateIndex(
                name: "ix_product_item_option_color_specification_id",
                schema: "orders",
                table: "product_item_option_color",
                column: "specification_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_item_option_numeric_specification_id",
                schema: "orders",
                table: "product_item_option_numeric",
                column: "specification_id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_item_options_product_item_product_item_id",
                schema: "orders",
                table: "product_item_options",
                column: "product_item_id",
                principalSchema: "orders",
                principalTable: "product_item",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_product_item_options_product_item_product_item_id",
                schema: "orders",
                table: "product_item_options");

            migrationBuilder.DropTable(
                name: "product_item_option_color",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "product_item_option_numeric",
                schema: "orders");

            migrationBuilder.RenameColumn(
                name: "product_item_id",
                schema: "orders",
                table: "product_item_options",
                newName: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_item_options_product_item_id",
                schema: "orders",
                table: "product_item_options",
                column: "id",
                principalSchema: "orders",
                principalTable: "product_item",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
