using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

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
                    value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permission", x => x.value);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "users",
                columns: table => new
                {
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.name);
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
                    confirmation_token = table.Column<string>(type: "text", nullable: false),
                    country_code = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false)
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
                    role_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    permission_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_permission", x => new { x.role_name, x.permission_name });
                    table.ForeignKey(
                        name: "fk_role_permission_permission_permission_name",
                        column: x => x.permission_name,
                        principalSchema: "users",
                        principalTable: "permission",
                        principalColumn: "value",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_role_permission_roles_role_name",
                        column: x => x.role_name,
                        principalSchema: "users",
                        principalTable: "roles",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "fk_token_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "users",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                schema: "users",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_name = table.Column<string>(type: "character varying(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_role", x => new { x.role_name, x.user_id });
                    table.ForeignKey(
                        name: "fk_user_role_roles_role_name",
                        column: x => x.role_name,
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

            migrationBuilder.CreateIndex(
                name: "ix_role_permission_permission_name",
                schema: "users",
                table: "role_permission",
                column: "permission_name");

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
                name: "ix_user_confirmation_token",
                schema: "users",
                table: "user",
                column: "confirmation_token");

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
