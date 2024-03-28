using System.Collections.Generic;
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
                name: "list_product_applied",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "type",
                table: "Vouchers");

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "Vouchers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "product_voucher",
                table: "Vouchers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_ProductId",
                table: "Vouchers",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_Products_ProductId",
                table: "Vouchers",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_Products_ProductId",
                table: "Vouchers");

            migrationBuilder.DropIndex(
                name: "IX_Vouchers_ProductId",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "product_voucher",
                table: "Vouchers");

            migrationBuilder.AddColumn<List<string>>(
                name: "list_product_applied",
                table: "Vouchers",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "type",
                table: "Vouchers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
