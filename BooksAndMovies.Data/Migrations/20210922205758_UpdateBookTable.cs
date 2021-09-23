using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksAndMovies.Data.Migrations
{
    public partial class UpdateBookTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_ImageLinks_ImageLinksId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "ImageLinks");

            migrationBuilder.DropIndex(
                name: "IX_Books_ImageLinksId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ImageLinksId",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "SmallThumbnail",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SmallThumbnail",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "ImageLinksId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ImageLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SmallThumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageLinks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_ImageLinksId",
                table: "Books",
                column: "ImageLinksId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_ImageLinks_ImageLinksId",
                table: "Books",
                column: "ImageLinksId",
                principalTable: "ImageLinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
