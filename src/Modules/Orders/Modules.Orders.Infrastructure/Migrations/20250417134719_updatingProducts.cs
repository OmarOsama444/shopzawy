using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Orders.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatingProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "bool_value",
                schema: "orders",
                table: "specification_option",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "data_type",
                schema: "orders",
                table: "specification_option",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "number_value",
                schema: "orders",
                table: "specification_option",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "string_value",
                schema: "orders",
                table: "specification_option",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bool_value",
                schema: "orders",
                table: "specification_option");

            migrationBuilder.DropColumn(
                name: "data_type",
                schema: "orders",
                table: "specification_option");

            migrationBuilder.DropColumn(
                name: "number_value",
                schema: "orders",
                table: "specification_option");

            migrationBuilder.DropColumn(
                name: "string_value",
                schema: "orders",
                table: "specification_option");
        }
    }
}
