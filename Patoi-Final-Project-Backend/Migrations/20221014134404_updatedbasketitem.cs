using Microsoft.EntityFrameworkCore.Migrations;

namespace Patoi_Final_Project_Backend.Migrations
{
    public partial class updatedbasketitem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_AspNetUsers_UserId",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_UserId",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BasketItems");

            migrationBuilder.AddColumn<int>(
                name: "CampaignId",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "BasketItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "BasketItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountPercent = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CampaignId",
                table: "Products",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_AppUserId",
                table: "BasketItems",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_AspNetUsers_AppUserId",
                table: "BasketItems",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Campaigns_CampaignId",
                table: "Products",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_AspNetUsers_AppUserId",
                table: "BasketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Campaigns_CampaignId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Products_CampaignId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_AppUserId",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "BasketItems");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BasketItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_UserId",
                table: "BasketItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_AspNetUsers_UserId",
                table: "BasketItems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
