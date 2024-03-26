using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lazada.Migrations
{
    /// <inheritdoc />
    public partial class _9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "shoprname_order",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "username_order",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "userId_order",
                table: "Orders",
                newName: "CartiteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CartiteId",
                table: "Orders",
                newName: "userId_order");

            migrationBuilder.AddColumn<string>(
                name: "shoprname_order",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "username_order",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
