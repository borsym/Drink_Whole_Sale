using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DrinkWholeSale.Web.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainCats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainCats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    MainCatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCats_MainCats_MainCatId",
                        column: x => x.MainCatId,
                        principalTable: "MainCats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Producer = table.Column<string>(maxLength: 30, nullable: false),
                    TypeNumber = table.Column<int>(nullable: false),
                    NetPrice = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Pack = table.Column<int>(nullable: false),
                    GrossPrice = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    SubCatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_SubCats_SubCatId",
                        column: x => x.SubCatId,
                        principalTable: "SubCats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubCatId",
                table: "Products",
                column: "SubCatId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCats_MainCatId",
                table: "SubCats",
                column: "MainCatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SubCats");

            migrationBuilder.DropTable(
                name: "MainCats");
        }
    }
}
