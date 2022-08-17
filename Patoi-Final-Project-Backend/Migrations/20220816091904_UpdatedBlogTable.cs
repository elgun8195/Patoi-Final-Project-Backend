using Microsoft.EntityFrameworkCore.Migrations;

namespace Patoi_Final_Project_Backend.Migrations
{
    public partial class UpdatedBlogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Blog",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Blog");
        }
    }
}
