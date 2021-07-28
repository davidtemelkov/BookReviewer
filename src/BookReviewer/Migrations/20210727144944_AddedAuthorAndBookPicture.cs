using Microsoft.EntityFrameworkCore.Migrations;

namespace BookReviewer.Migrations
{
    public partial class AddedAuthorAndBookPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAuthor",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "CoverUrl",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverUrl",
                table: "Books");

            migrationBuilder.AddColumn<bool>(
                name: "IsAuthor",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
