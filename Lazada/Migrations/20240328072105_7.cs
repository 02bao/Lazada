using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taka.Migrations
{
    /// <inheritdoc />
    public partial class _7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "list_user_applied",
                table: "Vouchers");

            migrationBuilder.AddColumn<long>(
                name: "user_applyid",
                table: "Vouchers",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_applyid",
                table: "Vouchers");

            migrationBuilder.AddColumn<List<string>>(
                name: "list_user_applied",
                table: "Vouchers",
                type: "text[]",
                nullable: true);
        }
    }
}
