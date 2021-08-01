using Microsoft.EntityFrameworkCore.Migrations;

namespace BookReviewer.Migrations
{
    public partial class BookList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Lists",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookLists_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookLists_Lists_ListId",
                        column: x => x.ListId,
                        principalTable: "Lists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lists_UserId",
                table: "Lists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookLists_BookId",
                table: "BookLists",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookLists_ListId",
                table: "BookLists",
                column: "ListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lists_AspNetUsers_UserId",
                table: "Lists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lists_AspNetUsers_UserId",
                table: "Lists");

            migrationBuilder.DropTable(
                name: "BookLists");

            migrationBuilder.DropIndex(
                name: "IX_Lists_UserId",
                table: "Lists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Lists");

            migrationBuilder.CreateTable(
                name: "UserBookLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    ListId = table.Column<int>(type: "int", nullable: false),
                    User = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBookLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBookLists_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserBookLists_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBookLists_Lists_ListId",
                        column: x => x.ListId,
                        principalTable: "Lists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBookLists_BookId",
                table: "UserBookLists",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBookLists_ListId",
                table: "UserBookLists",
                column: "ListId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBookLists_UserId1",
                table: "UserBookLists",
                column: "UserId1");
        }
    }
}
