using Microsoft.EntityFrameworkCore.Migrations;

namespace BookReviewer.Migrations
{
    public partial class FixedListRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lists_AspNetUsers_UserId1",
                table: "Lists");

            migrationBuilder.DropIndex(
                name: "IX_Lists_UserId1",
                table: "Lists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Lists");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Lists");

            migrationBuilder.CreateTable(
                name: "UserBookList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    User = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBookList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBookList_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserBookList_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBookList_Lists_ListId",
                        column: x => x.ListId,
                        principalTable: "Lists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBookList_BookId",
                table: "UserBookList",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBookList_ListId",
                table: "UserBookList",
                column: "ListId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBookList_UserId1",
                table: "UserBookList",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBookList");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Lists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Lists",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lists_UserId1",
                table: "Lists",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Lists_AspNetUsers_UserId1",
                table: "Lists",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
