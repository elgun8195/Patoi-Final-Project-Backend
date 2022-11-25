using Microsoft.EntityFrameworkCore.Migrations;

namespace Patoi_Final_Project_Backend.Migrations
{
    public partial class fakem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Fake_FakeId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_FakeId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FakeId",
                table: "ProductImages");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductImages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductImages");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FakeId",
                table: "ProductImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_FakeId",
                table: "ProductImages",
                column: "FakeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Fake_FakeId",
                table: "ProductImages",
                column: "FakeId",
                principalTable: "Fake",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
