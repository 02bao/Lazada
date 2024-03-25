using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lazada.Migrations
{
    /// <inheritdoc />
    public partial class _5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<string>>(
                name: "list_cart_item",
                table: "Orders",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "userId_order",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "list_cart_item",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "userId_order",
                table: "Orders");
        }
    }
}
