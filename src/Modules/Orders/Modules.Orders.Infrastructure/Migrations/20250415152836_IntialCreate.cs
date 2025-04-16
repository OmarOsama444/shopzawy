using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Orders.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IntialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "orders");

            migrationBuilder.CreateTable(
                name: "banner",
                schema: "orders",
                columns: table => new
                {
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    link = table.Column<string>(type: "text", nullable: false),
                    position = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_banner", x => x.title);
                });

            migrationBuilder.CreateTable(
                name: "brand",
                schema: "orders",
                columns: table => new
                {
                    brand_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    logo_url = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    featured = table.Column<bool>(type: "boolean", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_brand", x => x.brand_name);
                });

            migrationBuilder.CreateTable(
                name: "category",
                schema: "orders",
                columns: table => new
                {
                    category_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    parent_category_name = table.Column<string>(type: "character varying(100)", nullable: true),
                    description = table.Column<string>(type: "text", nullable: false),
                    order = table.Column<int>(type: "integer", nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: true),
                    category_path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category", x => x.category_name);
                    table.ForeignKey(
                        name: "fk_category_category_parent_category_name",
                        column: x => x.parent_category_name,
                        principalSchema: "orders",
                        principalTable: "category",
                        principalColumn: "category_name");
                });

            migrationBuilder.CreateTable(
                name: "color",
                schema: "orders",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_color", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "discount",
                schema: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    precentage = table.Column<float>(type: "real", nullable: true),
                    value = table.Column<float>(type: "real", nullable: true),
                    expiry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_discount", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vendor",
                schema: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    vendor_name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    logo_url = table.Column<string>(type: "text", nullable: false),
                    shiping_zone_name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vendor", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "category_specification",
                schema: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    data_type = table.Column<string>(type: "text", nullable: false),
                    category_name = table.Column<string>(type: "character varying(100)", nullable: false)
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
                name: "product",
                schema: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_name = table.Column<string>(type: "text", nullable: false),
                    l_description = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    stock_keeping_unit = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<float>(type: "real", nullable: false),
                    vendor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    brand_name = table.Column<string>(type: "character varying(100)", nullable: false),
                    category_name = table.Column<string>(type: "character varying(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_brand_brand_name",
                        column: x => x.brand_name,
                        principalSchema: "orders",
                        principalTable: "brand",
                        principalColumn: "brand_name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_category_category_name",
                        column: x => x.category_name,
                        principalSchema: "orders",
                        principalTable: "category",
                        principalColumn: "category_name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_vendor_vendor_id",
                        column: x => x.vendor_id,
                        principalSchema: "orders",
                        principalTable: "vendor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_category",
                schema: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_name = table.Column<string>(type: "character varying(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_category", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_category_category_category_name",
                        column: x => x.category_name,
                        principalSchema: "orders",
                        principalTable: "category",
                        principalColumn: "category_name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_category_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "orders",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_discount",
                schema: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    discount_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sale_price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_discount", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_discount_discount_discount_id",
                        column: x => x.discount_id,
                        principalSchema: "orders",
                        principalTable: "discount",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_discount_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "orders",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_specification_value",
                schema: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_spec_id = table.Column<Guid>(type: "uuid", nullable: false),
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
                name: "ix_category_parent_category_name",
                schema: "orders",
                table: "category",
                column: "parent_category_name");

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
                name: "ix_color_name",
                schema: "orders",
                table: "color",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_discount_expiry_date",
                schema: "orders",
                table: "discount",
                column: "expiry_date");

            migrationBuilder.CreateIndex(
                name: "ix_product_brand_name",
                schema: "orders",
                table: "product",
                column: "brand_name");

            migrationBuilder.CreateIndex(
                name: "ix_product_category_name",
                schema: "orders",
                table: "product",
                column: "category_name");

            migrationBuilder.CreateIndex(
                name: "ix_product_price",
                schema: "orders",
                table: "product",
                column: "price");

            migrationBuilder.CreateIndex(
                name: "ix_product_vendor_id",
                schema: "orders",
                table: "product",
                column: "vendor_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_category_category_name",
                schema: "orders",
                table: "product_category",
                column: "category_name");

            migrationBuilder.CreateIndex(
                name: "ix_product_category_product_id",
                schema: "orders",
                table: "product_category",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_discount_discount_id",
                schema: "orders",
                table: "product_discount",
                column: "discount_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_discount_product_id",
                schema: "orders",
                table: "product_discount",
                column: "product_id");

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

            migrationBuilder.CreateIndex(
                name: "ix_vendor_email",
                schema: "orders",
                table: "vendor",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_vendor_phone_number",
                schema: "orders",
                table: "vendor",
                column: "phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_vendor_vendor_name",
                schema: "orders",
                table: "vendor",
                column: "vendor_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "banner",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "color",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "product_category",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "product_discount",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "product_specification_value",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "discount",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "category_specification",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "product",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "brand",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "category",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "vendor",
                schema: "orders");
        }
    }
}
