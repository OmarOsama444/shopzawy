using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Modules.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class intialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "catalog");

            migrationBuilder.CreateTable(
                name: "banner",
                schema: "catalog",
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
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    logo_url = table.Column<string>(type: "text", nullable: false),
                    featured = table.Column<bool>(type: "boolean", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_brand", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "category",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parent_category_id = table.Column<int>(type: "integer", nullable: true),
                    order = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    path = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category", x => x.id);
                    table.ForeignKey(
                        name: "fk_category_category_parent_category_id",
                        column: x => x.parent_category_id,
                        principalSchema: "catalog",
                        principalTable: "category",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "category_statistics",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order = table.Column<int>(type: "integer", nullable: false),
                    parent_category_id = table.Column<int>(type: "integer", nullable: true),
                    child_category_ids = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
                    total_children = table.Column<int>(type: "integer", nullable: false),
                    total_products = table.Column<int>(type: "integer", nullable: false),
                    total_specs = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category_statistics", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "color",
                schema: "catalog",
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
                name: "inbox_consumer_message",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    handler_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inbox_consumer_message", x => new { x.id, x.handler_name });
                });

            migrationBuilder.CreateTable(
                name: "inbox_messages",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    occurred_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    processed_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    error = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inbox_messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "outbox_consumer_message",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    handler_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outbox_consumer_message", x => new { x.id, x.handler_name });
                });

            migrationBuilder.CreateTable(
                name: "outbox_messages",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    occurred_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    processed_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    error = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outbox_messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "specification",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    data_type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_specification", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "specification_statistics",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    data_type = table.Column<int>(type: "integer", nullable: false),
                    total_products = table.Column<int>(type: "integer", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_specification_statistics", x => new { x.id, x.value });
                });

            migrationBuilder.CreateTable(
                name: "vendor",
                schema: "catalog",
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
                name: "brand_translation",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    brand_id = table.Column<Guid>(type: "uuid", nullable: false),
                    lang_code = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_brand_translation", x => x.id);
                    table.ForeignKey(
                        name: "fk_brand_translation_brand_brand_id",
                        column: x => x.brand_id,
                        principalSchema: "catalog",
                        principalTable: "brand",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "category_translation",
                schema: "catalog",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    lang_code = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category_translation", x => new { x.category_id, x.lang_code });
                    table.ForeignKey(
                        name: "fk_category_translation_category_category_id",
                        column: x => x.category_id,
                        principalSchema: "catalog",
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "category_spec",
                schema: "catalog",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    spec_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category_spec", x => new { x.category_id, x.spec_id });
                    table.ForeignKey(
                        name: "fk_category_spec_category_category_id",
                        column: x => x.category_id,
                        principalSchema: "catalog",
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_category_spec_specification_spec_id",
                        column: x => x.spec_id,
                        principalSchema: "catalog",
                        principalTable: "specification",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "specification_option",
                schema: "catalog",
                columns: table => new
                {
                    specification_id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_specification_option", x => new { x.specification_id, x.value });
                    table.ForeignKey(
                        name: "fk_specification_option_specification_specification_id",
                        column: x => x.specification_id,
                        principalSchema: "catalog",
                        principalTable: "specification",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "specification_translation",
                schema: "catalog",
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
                        principalSchema: "catalog",
                        principalTable: "specification",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tags = table.Column<List<string>>(type: "text[]", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    weight_unit = table.Column<int>(type: "integer", nullable: false),
                    dimension_unit = table.Column<int>(type: "integer", nullable: false),
                    vendor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    brand_id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_brand_brand_id",
                        column: x => x.brand_id,
                        principalSchema: "catalog",
                        principalTable: "brand",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_category_category_id",
                        column: x => x.category_id,
                        principalSchema: "catalog",
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_vendor_vendor_id",
                        column: x => x.vendor_id,
                        principalSchema: "catalog",
                        principalTable: "vendor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_item",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    stock_keeping_unit = table.Column<string>(type: "text", nullable: false),
                    quantity_in_stock = table.Column<int>(type: "integer", nullable: false),
                    image_urls = table.Column<List<string>>(type: "text[]", nullable: false),
                    weight = table.Column<float>(type: "real", nullable: false),
                    width = table.Column<float>(type: "real", nullable: false),
                    length = table.Column<float>(type: "real", nullable: false),
                    height = table.Column<float>(type: "real", nullable: false),
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
                        principalSchema: "catalog",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_translation",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    lang_code = table.Column<int>(type: "integer", nullable: false),
                    product_name = table.Column<string>(type: "text", nullable: false),
                    long_description = table.Column<string>(type: "text", nullable: false),
                    short_description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_translation", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_translation_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "catalog",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_item_option_color",
                schema: "catalog",
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
                        principalSchema: "catalog",
                        principalTable: "color",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_item_option_color_product_item_product_item_id",
                        column: x => x.product_item_id,
                        principalSchema: "catalog",
                        principalTable: "product_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_item_option_color_specification_specification_id",
                        column: x => x.specification_id,
                        principalSchema: "catalog",
                        principalTable: "specification",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_item_option_numeric",
                schema: "catalog",
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
                        principalSchema: "catalog",
                        principalTable: "product_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_item_option_numeric_specification_specification_id",
                        column: x => x.specification_id,
                        principalSchema: "catalog",
                        principalTable: "specification",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_item_options",
                schema: "catalog",
                columns: table => new
                {
                    product_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    specification_id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_item_options", x => new { x.product_item_id, x.specification_id, x.value });
                    table.ForeignKey(
                        name: "fk_product_item_options_product_item_product_item_id",
                        column: x => x.product_item_id,
                        principalSchema: "catalog",
                        principalTable: "product_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_item_options_specification_option_specification_id_",
                        columns: x => new { x.specification_id, x.value },
                        principalSchema: "catalog",
                        principalTable: "specification_option",
                        principalColumns: new[] { "specification_id", "value" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "catalog",
                table: "category",
                columns: new[] { "id", "created_on", "order", "parent_category_id", "path" },
                values: new object[] { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2147483647, null, "" });

            migrationBuilder.InsertData(
                schema: "catalog",
                table: "category_translation",
                columns: new[] { "category_id", "lang_code", "description", "image_url", "name" },
                values: new object[] { 1, 1, "", "", "" });

            migrationBuilder.CreateIndex(
                name: "ix_banner_active",
                schema: "catalog",
                table: "banner",
                column: "active");

            migrationBuilder.CreateIndex(
                name: "ix_brand_translation_brand_id_lang_code",
                schema: "catalog",
                table: "brand_translation",
                columns: new[] { "brand_id", "lang_code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_category_parent_category_id",
                schema: "catalog",
                table: "category",
                column: "parent_category_id");

            migrationBuilder.CreateIndex(
                name: "ix_category_spec_spec_id",
                schema: "catalog",
                table: "category_spec",
                column: "spec_id");

            migrationBuilder.CreateIndex(
                name: "ix_category_statistics_created_on",
                schema: "catalog",
                table: "category_statistics",
                column: "created_on");

            migrationBuilder.CreateIndex(
                name: "ix_category_translation_category_id_lang_code",
                schema: "catalog",
                table: "category_translation",
                columns: new[] { "category_id", "lang_code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_color_name",
                schema: "catalog",
                table: "color",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_product_brand_id",
                schema: "catalog",
                table: "product",
                column: "brand_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_category_id",
                schema: "catalog",
                table: "product",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_created_on",
                schema: "catalog",
                table: "product",
                column: "created_on");

            migrationBuilder.CreateIndex(
                name: "ix_product_vendor_id",
                schema: "catalog",
                table: "product",
                column: "vendor_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_item_created_on",
                schema: "catalog",
                table: "product_item",
                column: "created_on");

            migrationBuilder.CreateIndex(
                name: "ix_product_item_product_id_stock_keeping_unit",
                schema: "catalog",
                table: "product_item",
                columns: new[] { "product_id", "stock_keeping_unit" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_product_item_stock_keeping_unit",
                schema: "catalog",
                table: "product_item",
                column: "stock_keeping_unit");

            migrationBuilder.CreateIndex(
                name: "ix_product_item_option_color_color_code",
                schema: "catalog",
                table: "product_item_option_color",
                column: "color_code");

            migrationBuilder.CreateIndex(
                name: "ix_product_item_option_color_specification_id",
                schema: "catalog",
                table: "product_item_option_color",
                column: "specification_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_item_option_numeric_specification_id",
                schema: "catalog",
                table: "product_item_option_numeric",
                column: "specification_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_item_options_specification_id_value",
                schema: "catalog",
                table: "product_item_options",
                columns: new[] { "specification_id", "value" });

            migrationBuilder.CreateIndex(
                name: "ix_product_translation_product_id_lang_code",
                schema: "catalog",
                table: "product_translation",
                columns: new[] { "product_id", "lang_code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_specification_statistics_created_on_utc",
                schema: "catalog",
                table: "specification_statistics",
                column: "created_on_utc");

            migrationBuilder.CreateIndex(
                name: "ix_specification_statistics_id",
                schema: "catalog",
                table: "specification_statistics",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_specification_statistics_total_products",
                schema: "catalog",
                table: "specification_statistics",
                column: "total_products");

            migrationBuilder.CreateIndex(
                name: "ix_specification_statistics_value",
                schema: "catalog",
                table: "specification_statistics",
                column: "value");

            migrationBuilder.CreateIndex(
                name: "ix_specification_translation_name",
                schema: "catalog",
                table: "specification_translation",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_specification_translation_spec_id_lang_code",
                schema: "catalog",
                table: "specification_translation",
                columns: new[] { "spec_id", "lang_code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_vendor_email",
                schema: "catalog",
                table: "vendor",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_vendor_phone_number",
                schema: "catalog",
                table: "vendor",
                column: "phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_vendor_vendor_name",
                schema: "catalog",
                table: "vendor",
                column: "vendor_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "banner",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "brand_translation",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "category_spec",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "category_statistics",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "category_translation",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "inbox_consumer_message",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "inbox_messages",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "outbox_consumer_message",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "outbox_messages",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "product_item_option_color",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "product_item_option_numeric",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "product_item_options",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "product_translation",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "specification_statistics",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "specification_translation",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "color",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "product_item",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "specification_option",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "product",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "specification",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "brand",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "category",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "vendor",
                schema: "catalog");
        }
    }
}
