using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Microservices.ShoppingCartAPI.Migrations
{
    public partial class addCartHeaders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartHeaders",
                columns: table => new
                {
                    CartHeaderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CouponCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartHeaders", x => x.CartHeaderId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartHeaders");
        }
    }
}
