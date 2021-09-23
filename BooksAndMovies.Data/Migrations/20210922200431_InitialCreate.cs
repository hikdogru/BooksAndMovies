using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksAndMovies.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SmallThumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookImage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageCount = table.Column<int>(type: "int", nullable: false),
                    AverageRating = table.Column<double>(type: "float", nullable: false),
                    ImageLinksId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_BookImage_ImageLinksId",
                        column: x => x.ImageLinksId,
                        principalTable: "BookImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_ImageLinksId",
                table: "Books",
                column: "ImageLinksId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "BookImage");
        }
    }
}
