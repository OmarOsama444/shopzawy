using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Orders.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ana3ayzamoot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_product_item_options_category_specification_option_category",
                schema: "orders",
                table: "product_item_options");

            migrationBuilder.DropTable(
                name: "category_specification_option",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "category_specification",
                schema: "orders");

            migrationBuilder.CreateTable(
                name: "specification",
                schema: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    data_type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_specification", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "category_spec",
                schema: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_name = table.Column<string>(type: "character varying(100)", nullable: false),
                    spec_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category_spec", x => x.id);
                    table.ForeignKey(
                        name: "fk_category_spec_category_category_name",
                        column: x => x.category_name,
                        principalSchema: "orders",
                        principalTable: "category",
                        principalColumn: "category_name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_category_spec_specification_spec_id",
                        column: x => x.spec_id,
                        principalSchema: "orders",
                        principalTable: "specification",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "specification_option",
                schema: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    specification_id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_specification_option", x => x.id);
                    table.ForeignKey(
                        name: "fk_specification_option_specification_specification_id",
                        column: x => x.specification_id,
                        principalSchema: "orders",
                        principalTable: "specification",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_category_spec_category_name",
                schema: "orders",
                table: "category_spec",
                column: "category_name");

            migrationBuilder.CreateIndex(
                name: "ix_category_spec_spec_id",
                schema: "orders",
                table: "category_spec",
                column: "spec_id");

            migrationBuilder.CreateIndex(
                name: "ix_specification_name",
                schema: "orders",
                table: "specification",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_specification_option_specification_id",
                schema: "orders",
                table: "specification_option",
                column: "specification_id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_item_options_specification_option_category_specific",
                schema: "orders",
                table: "product_item_options",
                column: "category_specification_option_id",
                principalSchema: "orders",
                principalTable: "specification_option",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_product_item_options_specification_option_category_specific",
                schema: "orders",
                table: "product_item_options");

            migrationBuilder.DropTable(
                name: "category_spec",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "specification_option",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "specification",
                schema: "orders");

            migrationBuilder.CreateTable(
                name: "category_specification",
                schema: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_name = table.Column<string>(type: "character varying(100)", nullable: false),
                    data_type = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category_specification", x => x.id);
                    table.ForeignKey(
                        name: "fk_category_specification_category_category_name",
                        column: x => x.category_name,
                        principalSchema: "orders",
                        principalTable: "category",
                        principalColumn: "category_name",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "ix_category_specification_category_name",
                schema: "orders",
                table: "category_specification",
                column: "category_name");

            migrationBuilder.CreateIndex(
                name: "ix_category_specification_name",
                schema: "orders",
                table: "category_specification",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_category_specification_name_category_name",
                schema: "orders",
                table: "category_specification",
                columns: new[] { "name", "category_name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_category_specification_option_category_specification_id",
                schema: "orders",
                table: "category_specification_option",
                column: "category_specification_id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_item_options_category_specification_option_category",
                schema: "orders",
                table: "product_item_options",
                column: "category_specification_option_id",
                principalSchema: "orders",
                principalTable: "category_specification_option",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
