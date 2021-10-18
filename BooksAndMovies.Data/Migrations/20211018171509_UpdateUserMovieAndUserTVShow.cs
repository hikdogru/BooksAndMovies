using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksAndMovies.Data.Migrations
{
    public partial class UpdateUserMovieAndUserTVShow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "UserTVShows",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "UserMovies",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "UserTVShows");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "UserMovies");
        }
    }
}
