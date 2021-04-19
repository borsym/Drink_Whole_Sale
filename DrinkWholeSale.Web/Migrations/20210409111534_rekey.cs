using Microsoft.EntityFrameworkCore.Migrations;

namespace DrinkWholeSale.Web.Migrations
{
    public partial class rekey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_Orders_OrderId",
                table: "ShoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCart_OrderId",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ShoppingCart");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ShoppingCart",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ShoppingCart");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "ShoppingCart",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_OrderId",
                table: "ShoppingCart",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_Orders_OrderId",
                table: "ShoppingCart",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
