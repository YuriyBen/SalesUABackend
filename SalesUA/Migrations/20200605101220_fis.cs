using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesUA.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shop",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    ImagePath = table.Column<string>(unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shop", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    OldPrice = table.Column<decimal>(type: "smallmoney", nullable: false),
                    NewPrice = table.Column<decimal>(type: "smallmoney", nullable: false),
                    DiscountPercent = table.Column<byte>(nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 1000, nullable: false),
                    ImagePath = table.Column<string>(unicode: false, nullable: false),
                    ShopId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Product__ShopId__267ABA7A",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ShopId",
                table: "Product",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "UQ_Title",
                table: "Shop",
                column: "Title",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Shop");
        }
    }
}
