using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Users.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "USERS");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "USERS");

            migrationBuilder.RenameTable(
                name: "unverifiedUsers",
                newName: "unverifiedUsers",
                newSchema: "USERS");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                newName: "RefreshTokens",
                newSchema: "USERS");

            migrationBuilder.RenameTable(
                name: "outboxConsumerMessages",
                newName: "outboxConsumerMessages",
                newSchema: "USERS");

            migrationBuilder.RenameTable(
                name: "outbox_messages",
                newName: "outbox_messages",
                newSchema: "USERS");

            migrationBuilder.RenameTable(
                name: "emailVerificationTokens",
                newName: "emailVerificationTokens",
                newSchema: "USERS");

            migrationBuilder.CreateTable(
                name: "inbox_messages",
                schema: "USERS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    OccurredOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Error = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inbox_messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "inboxConsumerMessages",
                schema: "USERS",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HandlerName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inboxConsumerMessages", x => new { x.id, x.HandlerName });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inbox_messages",
                schema: "USERS");

            migrationBuilder.DropTable(
                name: "inboxConsumerMessages",
                schema: "USERS");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "USERS",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "unverifiedUsers",
                schema: "USERS",
                newName: "unverifiedUsers");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                schema: "USERS",
                newName: "RefreshTokens");

            migrationBuilder.RenameTable(
                name: "outboxConsumerMessages",
                schema: "USERS",
                newName: "outboxConsumerMessages");

            migrationBuilder.RenameTable(
                name: "outbox_messages",
                schema: "USERS",
                newName: "outbox_messages");

            migrationBuilder.RenameTable(
                name: "emailVerificationTokens",
                schema: "USERS",
                newName: "emailVerificationTokens");
        }
    }
}
