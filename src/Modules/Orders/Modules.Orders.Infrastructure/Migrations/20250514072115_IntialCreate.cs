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
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    link = table.Column<string>(type: "text", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    position = table.Column<int>(type: "integer", nullable: false),
                    size = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_banner", x => x.id);
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
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    parent_category_id = table.Column<Guid>(type: "uuid", nullable: true),
                    order = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category", x => x.id);
                    table.ForeignKey(
                        name: "fk_category_category_parent_category_id",
                        column: x => x.parent_category_id,
                        principalSchema: "orders",
                        principalTable: "category",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "color",
                schema: "orders",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_color", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "specification",
                schema: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    data_type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_specification", x => x.id);
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
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vendor", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "category_translation",
                schema: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    lang_code = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category_translation", x => x.id);
                    table.ForeignKey(
                        name: "fk_category_translation_category_category_id",
                        column: x => x.category_id,
                        principalSchema: "orders",
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "category_spec",
                schema: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    spec_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category_spec", x => x.id);
                    table.ForeignKey(
                        name: "fk_category_spec_category_category_id",
                        column: x => x.category_id,
                        principalSchema: "orders",
                        principalTable: "category",
                        principalColumn: "id",
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
                    string_value = table.Column<string>(type: "text", nullable: true),
                    number_value = table.Column<double>(type: "double precision", nullable: true),
                    bool_value = table.Column<bool>(type: "boolean", nullable: true),
                    data_type = table.Column<string>(type: "text", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "specification_translation",
                schema: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    spec_id = table.Column<Guid>(type: "uuid", nullable: false),
                    lang_code = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_specification_translation", x => x.id);
                    table.ForeignKey(
                        name: "fk_specification_translation_specification_spec_id",
                        column: x => x.spec_id,
                        principalSchema: "orders",
                        principalTable: "specification",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product",
                schema: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_name = table.Column<string>(type: "text", nullable: false),
                    long_description = table.Column<string>(type: "text", nullable: false),
                    short_description = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    weight_unit = table.Column<int>(type: "integer", nullable: false),
                    weight = table.Column<float>(type: "real", nullable: false),
                    dimension_unit = table.Column<int>(type: "integer", nullable: false),
                    width = table.Column<float>(type: "real", nullable: false),
                    length = table.Column<float>(type: "real", nullable: false),
                    height = table.Column<float>(type: "real", nullable: false),
                    price = table.Column<float>(type: "real", nullable: false),
                    Tags = table.Column<string>(type: "TEXT", nullable: false),
                    vendor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    brand_name = table.Column<string>(type: "character varying(100)", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false)
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
                        name: "fk_product_category_category_id",
                        column: x => x.category_id,
                        principalSchema: "orders",
                        principalTable: "category",
                        principalColumn: "id",
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
                name: "product_item",
                schema: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    stock_keeping_unit = table.Column<string>(type: "text", nullable: false),
                    quantity_in_stock = table.Column<int>(type: "integer", nullable: false),
                    image_urls = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<float>(type: "real", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                        name: "fk_product_item_options_product_item_product_item_id",
                        column: x => x.product_item_id,
                        principalSchema: "orders",
                        principalTable: "product_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_item_options_specification_option_category_specific",
                        column: x => x.category_specification_option_id,
                        principalSchema: "orders",
                        principalTable: "specification_option",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_banner_active",
                schema: "orders",
                table: "banner",
                column: "active");

            migrationBuilder.CreateIndex(
                name: "ix_category_order",
                schema: "orders",
                table: "category",
                column: "order",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_category_parent_category_id",
                schema: "orders",
                table: "category",
                column: "parent_category_id");

            migrationBuilder.CreateIndex(
                name: "ix_category_spec_category_id",
                schema: "orders",
                table: "category_spec",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_category_spec_spec_id",
                schema: "orders",
                table: "category_spec",
                column: "spec_id");

            migrationBuilder.CreateIndex(
                name: "ix_category_translation_category_id_lang_code",
                schema: "orders",
                table: "category_translation",
                columns: new[] { "category_id", "lang_code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_category_translation_name",
                schema: "orders",
                table: "category_translation",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_color_name",
                schema: "orders",
                table: "color",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_product_brand_name",
                schema: "orders",
                table: "product",
                column: "brand_name");

            migrationBuilder.CreateIndex(
                name: "ix_product_category_id",
                schema: "orders",
                table: "product",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_created_on",
                schema: "orders",
                table: "product",
                column: "created_on");

            migrationBuilder.CreateIndex(
                name: "ix_product_vendor_id",
                schema: "orders",
                table: "product",
                column: "vendor_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_item_created_on",
                schema: "orders",
                table: "product_item",
                column: "created_on");

            migrationBuilder.CreateIndex(
                name: "ix_product_item_product_id",
                schema: "orders",
                table: "product_item",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_item_stock_keeping_unit",
                schema: "orders",
                table: "product_item",
                column: "stock_keeping_unit");

            migrationBuilder.CreateIndex(
                name: "ix_product_item_options_category_specification_option_id",
                schema: "orders",
                table: "product_item_options",
                column: "category_specification_option_id");

            migrationBuilder.CreateIndex(
                name: "ix_specification_option_specification_id",
                schema: "orders",
                table: "specification_option",
                column: "specification_id");

            migrationBuilder.CreateIndex(
                name: "ix_specification_translation_name",
                schema: "orders",
                table: "specification_translation",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_specification_translation_spec_id_lang_code",
                schema: "orders",
                table: "specification_translation",
                columns: new[] { "spec_id", "lang_code" },
                unique: true);

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
                name: "category_spec",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "category_translation",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "color",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "product_item_options",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "specification_translation",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "product_item",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "specification_option",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "product",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "specification",
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
