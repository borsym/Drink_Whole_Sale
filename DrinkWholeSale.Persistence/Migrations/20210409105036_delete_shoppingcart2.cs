using Microsoft.EntityFrameworkCore.Migrations;

namespace DrinkWholeSale.Persistence.Migrations
{
    public partial class delete_shoppingcart2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ShoppingCart");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ShoppingCart",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
