using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_API.Migrations
{
    public partial class AddingMigration12282022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthors");

            migrationBuilder.DropTable(
                name: "BookGenres");

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

            migrationBuilder.CreateTable(
                name: "BooksAuthors",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksAuthors", x => new { x.BookId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_BooksAuthors_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BooksAuthors_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BooksGenres",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksGenres", x => new { x.BookId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_BooksGenres_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BooksGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genres_BooksBookId",
                table: "Genres",
                column: "BooksBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_BooksBookId",
                table: "Authors",
                column: "BooksBookId");

            migrationBuilder.CreateIndex(
                name: "IX_BooksAuthors_AuthorId",
                table: "BooksAuthors",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BooksGenres_GenreId",
                table: "BooksGenres",
                column: "GenreId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Books_BooksBookId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Books_BooksBookId",
                table: "Genres");

            migrationBuilder.DropTable(
                name: "BooksAuthors");

            migrationBuilder.DropTable(
                name: "BooksGenres");

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

            migrationBuilder.CreateTable(
                name: "BookAuthors",
                columns: table => new
                {
                    AuthorsAuthorId = table.Column<int>(type: "int", nullable: false),
                    BooksBookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthors", x => new { x.AuthorsAuthorId, x.BooksBookId });
                    table.ForeignKey(
                        name: "FK_BookAuthors_Authors_AuthorsAuthorId",
                        column: x => x.AuthorsAuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthors_Books_BooksBookId",
                        column: x => x.BooksBookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookGenres",
                columns: table => new
                {
                    BooksBookId = table.Column<int>(type: "int", nullable: false),
                    GenresGenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenres", x => new { x.BooksBookId, x.GenresGenreId });
                    table.ForeignKey(
                        name: "FK_BookGenres_Books_BooksBookId",
                        column: x => x.BooksBookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookGenres_Genres_GenresGenreId",
                        column: x => x.GenresGenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_BooksBookId",
                table: "BookAuthors",
                column: "BooksBookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_GenresGenreId",
                table: "BookGenres",
                column: "GenresGenreId");
        }
    }
}
