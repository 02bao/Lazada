using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taka.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "voucher",
                table: "Orders");

            migrationBuilder.AddColumn<long>(
                name: "quantityorder",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quantityorder",
                table: "Orders");

            migrationBuilder.AddColumn<List<long>>(
                name: "voucher",
                table: "Orders",
                type: "bigint[]",
                nullable: true);
        }
    }
}
