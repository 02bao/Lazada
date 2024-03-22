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
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Shops_Shopid",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Shopid",
                table: "Orders",
                newName: "ShopId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_Shopid",
                table: "Orders",
                newName: "IX_Orders_ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Shops_ShopId",
                table: "Orders",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Shops_ShopId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ShopId",
                table: "Orders",
                newName: "Shopid");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ShopId",
                table: "Orders",
                newName: "IX_Orders_Shopid");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Shops_Shopid",
                table: "Orders",
                column: "Shopid",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
