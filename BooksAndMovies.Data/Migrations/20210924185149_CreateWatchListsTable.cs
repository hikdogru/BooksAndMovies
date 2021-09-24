using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksAndMovies.Data.Migrations
{
    public partial class CreateWatchListsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieWatchLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PosterPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Popularity = table.Column<double>(type: "float", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    BackdropPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoteCount = table.Column<int>(type: "int", nullable: false),
                    VoteAverage = table.Column<double>(type: "float", nullable: false),
                    Overview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieWatchLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TVShowWatchLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PosterPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Popularity = table.Column<double>(type: "float", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    BackdropPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoteCount = table.Column<int>(type: "int", nullable: false),
                    VoteAverage = table.Column<double>(type: "float", nullable: false),
                    Overview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstAirDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVShowWatchLists", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieWatchLists");

            migrationBuilder.DropTable(
                name: "TVShowWatchLists");
        }
    }
}
