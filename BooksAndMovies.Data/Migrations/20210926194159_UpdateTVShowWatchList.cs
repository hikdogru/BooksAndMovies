using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksAndMovies.Data.Migrations
{
    public partial class UpdateTVShowWatchList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "TVShowWatchLists");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "MovieWatchLists");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "TVShowWatchLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "MovieWatchLists",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
