using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taka.Migrations
{
    /// <inheritdoc />
    public partial class _5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quantityorder",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "quantityorder",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
