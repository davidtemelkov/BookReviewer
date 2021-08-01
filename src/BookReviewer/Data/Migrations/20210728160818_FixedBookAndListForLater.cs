using Microsoft.EntityFrameworkCore.Migrations;

namespace BookReviewer.Migrations
{
    public partial class FixedBookAndListForLater : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Lists_ListId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_ListId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ListId",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ListId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_ListId",
                table: "Books",
                column: "ListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Lists_ListId",
                table: "Books",
                column: "ListId",
                principalTable: "Lists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
