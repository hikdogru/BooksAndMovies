using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksAndMovies.Data.Migrations
{
    public partial class AddImageLinksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookImage_ImageLinksId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookImage",
                table: "BookImage");

            migrationBuilder.RenameTable(
                name: "BookImage",
                newName: "ImageLinks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageLinks",
                table: "ImageLinks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_ImageLinks_ImageLinksId",
                table: "Books",
                column: "ImageLinksId",
                principalTable: "ImageLinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_ImageLinks_ImageLinksId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageLinks",
                table: "ImageLinks");

            migrationBuilder.RenameTable(
                name: "ImageLinks",
                newName: "BookImage");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookImage",
                table: "BookImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookImage_ImageLinksId",
                table: "Books",
                column: "ImageLinksId",
                principalTable: "BookImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
