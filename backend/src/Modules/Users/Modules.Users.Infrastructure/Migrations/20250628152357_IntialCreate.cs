using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Modules.Users.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IntialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "users");

            migrationBuilder.CreateTable(
                name: "inbox_consumer_message",
                schema: "users",
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
                schema: "users",
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
                schema: "users",
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
                schema: "users",
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
                name: "permission",
                schema: "users",
                columns: table => new
                {
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    module = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permission", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "users",
                columns: table => new
                {
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "token",
                schema: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    token_type = table.Column<int>(type: "integer", nullable: false),
                    expire_date_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_token", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    date_of_creation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    country_code = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    last_login_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role_permission",
                schema: "users",
                columns: table => new
                {
                    role_id = table.Column<string>(type: "character varying(100)", nullable: false),
                    permission_id = table.Column<string>(type: "character varying(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_permission", x => new { x.role_id, x.permission_id });
                    table.ForeignKey(
                        name: "fk_role_permission_permission_permission_id",
                        column: x => x.permission_id,
                        principalSchema: "users",
                        principalTable: "permission",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_role_permission_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "users",
                        principalTable: "roles",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                schema: "users",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<string>(type: "character varying(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_role", x => new { x.role_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_user_role_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "users",
                        principalTable: "roles",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_role_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "users",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "permission",
                columns: new[] { "name", "active", "created_on_utc", "module" },
                values: new object[,]
                {
                    { "auth:login", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "users" },
                    { "banner:create", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "orders" },
                    { "banner:delete", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "orders" },
                    { "banner:read", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "orders" },
                    { "brand:create", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "orders" },
                    { "brand:read", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "orders" },
                    { "brand:update", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "orders" },
                    { "category:create", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "orders" },
                    { "category:delete", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "orders" },
                    { "category:read", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "orders" },
                    { "category:update", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "orders" },
                    { "color:create", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "orders" },
                    { "color:read", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "orders" },
                    { "permission:create", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "users" },
                    { "permission:read", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "users" },
                    { "permission:update", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "users" },
                    { "product:create", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "orders" },
                    { "product:item:create", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "orders" },
                    { "product:item:delete", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "orders" },
                    { "product:item:read", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "orders" },
                    { "role:create", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "users" },
                    { "role:permission:update", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "users" },
                    { "role:read", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "users" },
                    { "spec:create", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "orders" },
                    { "spec:read", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "orders" },
                    { "spec:update", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "orders" },
                    { "user:create", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "users" },
                    { "user:role:update", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "users" },
                    { "vendor:create", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "orders" },
                    { "vendor:read", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "orders" },
                    { "vendor:update", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "orders" }
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "roles",
                columns: new[] { "name", "created_on_utc" },
                values: new object[,]
                {
                    { "admin", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "default", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "guest", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "user",
                columns: new[] { "id", "country_code", "date_of_creation", "email", "email_confirmed", "first_name", "last_login_date", "last_name", "password_hash", "phone_number", "phone_number_confirmed" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", true, "Admin", null, "User", "AQAAAAIAAYagAAAAEJOqYyDPiMJFm1mVQx3qEAyLF9qqYyRZQamJuHF11binnXBQGuCSBJu+8T4lDkxPxg==", null, false });

            migrationBuilder.InsertData(
                schema: "users",
                table: "role_permission",
                columns: new[] { "permission_id", "role_id" },
                values: new object[,]
                {
                    { "banner:create", "admin" },
                    { "banner:delete", "admin" },
                    { "banner:read", "admin" },
                    { "brand:create", "admin" },
                    { "brand:read", "admin" },
                    { "brand:update", "admin" },
                    { "category:create", "admin" },
                    { "category:delete", "admin" },
                    { "category:read", "admin" },
                    { "category:update", "admin" },
                    { "color:create", "admin" },
                    { "color:read", "admin" },
                    { "permission:create", "admin" },
                    { "permission:read", "admin" },
                    { "permission:update", "admin" },
                    { "product:create", "admin" },
                    { "product:item:create", "admin" },
                    { "product:item:delete", "admin" },
                    { "product:item:read", "admin" },
                    { "role:create", "admin" },
                    { "role:permission:update", "admin" },
                    { "role:read", "admin" },
                    { "spec:create", "admin" },
                    { "spec:read", "admin" },
                    { "spec:update", "admin" },
                    { "user:create", "admin" },
                    { "user:role:update", "admin" },
                    { "vendor:create", "admin" },
                    { "vendor:read", "admin" },
                    { "vendor:update", "admin" },
                    { "banner:read", "default" },
                    { "brand:read", "default" },
                    { "category:read", "default" },
                    { "color:read", "default" },
                    { "product:item:read", "default" },
                    { "spec:read", "default" },
                    { "vendor:read", "default" },
                    { "auth:login", "guest" },
                    { "banner:read", "guest" },
                    { "brand:read", "guest" },
                    { "category:read", "guest" },
                    { "product:item:read", "guest" },
                    { "user:create", "guest" }
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "user_role",
                columns: new[] { "role_id", "user_id" },
                values: new object[] { "admin", new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.CreateIndex(
                name: "ix_role_permission_permission_id",
                schema: "users",
                table: "role_permission",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "ix_roles_name",
                schema: "users",
                table: "roles",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_token_user_id",
                schema: "users",
                table: "token",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_token_value_token_type",
                schema: "users",
                table: "token",
                columns: new[] { "value", "token_type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_role_user_id",
                schema: "users",
                table: "user_role",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inbox_consumer_message",
                schema: "users");

            migrationBuilder.DropTable(
                name: "inbox_messages",
                schema: "users");

            migrationBuilder.DropTable(
                name: "outbox_consumer_message",
                schema: "users");

            migrationBuilder.DropTable(
                name: "outbox_messages",
                schema: "users");

            migrationBuilder.DropTable(
                name: "role_permission",
                schema: "users");

            migrationBuilder.DropTable(
                name: "token",
                schema: "users");

            migrationBuilder.DropTable(
                name: "user_role",
                schema: "users");

            migrationBuilder.DropTable(
                name: "permission",
                schema: "users");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "users");

            migrationBuilder.DropTable(
                name: "user",
                schema: "users");
        }
    }
}
