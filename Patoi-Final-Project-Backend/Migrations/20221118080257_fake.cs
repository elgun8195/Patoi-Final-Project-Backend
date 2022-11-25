using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Patoi_Final_Project_Backend.Migrations
{
    public partial class fake : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "FakeId",
                table: "ProductImages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Fake",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    DiscountPercent = table.Column<double>(nullable: false),
                    DiscountPrice = table.Column<double>(nullable: false),
                    TaxPercent = table.Column<double>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    IsAvailability = table.Column<bool>(nullable: false),
                    IsSpecial = table.Column<bool>(nullable: false),
                    IsFeatured = table.Column<bool>(nullable: false),
                    Bestseller = table.Column<bool>(nullable: false),
                    NewArrival = table.Column<bool>(nullable: false),
                    InStock = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fake", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Fake_FakeId",
                table: "ProductImages");

            migrationBuilder.DropTable(
                name: "Fake");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_FakeId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "FakeId",
                table: "ProductImages");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductImages",
                type: "int",
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
    }
}
