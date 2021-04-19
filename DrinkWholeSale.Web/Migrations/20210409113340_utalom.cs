using Microsoft.EntityFrameworkCore.Migrations;

namespace DrinkWholeSale.Web.Migrations
{
    public partial class utalom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Guests_GuestId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "GuestId",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Guests_GuestId",
                table: "Orders",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Guests_GuestId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "GuestId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Guests_GuestId",
                table: "Orders",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
