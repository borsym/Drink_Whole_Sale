using Microsoft.EntityFrameworkCore.Migrations;

namespace DrinkWholeSale.Persistence.Migrations
{
    public partial class added_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ShoppingCart",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ShoppingCart");
        }
    }
}
