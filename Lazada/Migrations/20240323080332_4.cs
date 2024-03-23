using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lazada.Migrations
{
    /// <inheritdoc />
    public partial class _4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "CartItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "CartItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CartItems");
        }
    }
}
