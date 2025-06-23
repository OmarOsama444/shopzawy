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
                    { "auth:login", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Users" },
                    { "banner:create", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Orders" },
                    { "banner:delete", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Orders" },
                    { "banner:read", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Orders" },
                    { "brand:create", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Orders" },
                    { "brand:read", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Orders" },
                    { "brand:update", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Orders" },
                    { "category:create", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Orders" },
                    { "category:delete", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Orders" },
                    { "category:read", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Orders" },
                    { "category:update", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Orders" },
                    { "color:create", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Orders" },
                    { "color:read", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Orders" },
                    { "permission:create", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Users" },
                    { "permission:read", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Users" },
                    { "permission:update", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Users" },
                    { "product:create", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Orders" },
                    { "product:item:create", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Orders" },
                    { "product:item:delete", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Orders" },
                    { "product:item:read", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Orders" },
                    { "role:create", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Users" },
                    { "role:permission:update", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Users" },
                    { "role:read", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Users" },
                    { "spec:create", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Orders" },
                    { "spec:read", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Orders" },
                    { "spec:update", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Orders" },
                    { "user:create", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Users" },
                    { "user:role:update", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Users" },
                    { "vendor:create", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Orders" },
                    { "vendor:read", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Orders" },
                    { "vendor:update", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Orders" }
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "roles",
                columns: new[] { "name", "created_on_utc" },
                values: new object[,]
                {
                    { "Admin", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "Default", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "Guest", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
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
                    { "banner:create", "Admin" },
                    { "banner:delete", "Admin" },
                    { "banner:read", "Admin" },
                    { "brand:create", "Admin" },
                    { "brand:read", "Admin" },
                    { "brand:update", "Admin" },
                    { "category:create", "Admin" },
                    { "category:delete", "Admin" },
                    { "category:read", "Admin" },
                    { "category:update", "Admin" },
                    { "color:create", "Admin" },
                    { "color:read", "Admin" },
                    { "product:create", "Admin" },
                    { "product:item:create", "Admin" },
                    { "product:item:delete", "Admin" },
                    { "product:item:read", "Admin" },
                    { "spec:create", "Admin" },
                    { "spec:read", "Admin" },
                    { "spec:update", "Admin" },
                    { "vendor:create", "Admin" },
                    { "vendor:read", "Admin" },
                    { "vendor:update", "Admin" },
                    { "banner:read", "Default" },
                    { "brand:read", "Default" },
                    { "category:read", "Default" },
                    { "color:read", "Default" },
                    { "product:item:read", "Default" },
                    { "spec:read", "Default" },
                    { "vendor:read", "Default" },
                    { "auth:login", "Guest" },
                    { "banner:read", "Guest" },
                    { "brand:read", "Guest" },
                    { "category:read", "Guest" },
                    { "product:item:read", "Guest" },
                    { "user:create", "Guest" }
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "user_role",
                columns: new[] { "role_id", "user_id" },
                values: new object[] { "Admin", new Guid("11111111-1111-1111-1111-111111111111") });

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
