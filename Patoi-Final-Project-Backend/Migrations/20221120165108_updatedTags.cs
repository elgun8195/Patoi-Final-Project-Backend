using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Patoi_Final_Project_Backend.Migrations
{
    public partial class updatedTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Tags",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Tags",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Tags",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Tags",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Tags",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ParentId",
                table: "Tags",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Tags_ParentId",
                table: "Tags",
                column: "ParentId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Tags_ParentId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ParentId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Tags");
        }
    }
}
