using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_API.Migrations
{
    public partial class AddingFixingRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Books_BooksBookId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Books_BooksBookId",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Genres_BooksBookId",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Authors_BooksBookId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "BooksBookId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "BooksBookId",
                table: "Authors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BooksBookId",
                table: "Genres",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BooksBookId",
                table: "Authors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_BooksBookId",
                table: "Genres",
                column: "BooksBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_BooksBookId",
                table: "Authors",
                column: "BooksBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Books_BooksBookId",
                table: "Authors",
                column: "BooksBookId",
                principalTable: "Books",
                principalColumn: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Books_BooksBookId",
                table: "Genres",
                column: "BooksBookId",
                principalTable: "Books",
                principalColumn: "BookId");
        }
    }
}
