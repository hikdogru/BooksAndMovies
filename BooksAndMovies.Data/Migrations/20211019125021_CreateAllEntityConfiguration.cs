using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksAndMovies.Data.Migrations
{
    public partial class CreateAllEntityConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PageCount = table.Column<int>(type: "int", nullable: false),
                    AverageRating = table.Column<double>(type: "float", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SmallThumbnail = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DatabaseSavingType = table.Column<int>(type: "int", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReleaseDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OriginalTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RealId = table.Column<int>(type: "int", nullable: false),
                    PosterPath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OriginalLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Popularity = table.Column<double>(type: "float", nullable: false),
                    BackdropPath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VoteCount = table.Column<int>(type: "int", nullable: false),
                    VoteAverage = table.Column<double>(type: "float", nullable: false),
                    Overview = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DatabaseSavingType = table.Column<int>(type: "int", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TVShows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OriginalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FirstAirDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RealId = table.Column<int>(type: "int", nullable: false),
                    PosterPath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OriginalLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Popularity = table.Column<double>(type: "float", nullable: false),
                    BackdropPath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VoteCount = table.Column<int>(type: "int", nullable: false),
                    VoteAverage = table.Column<double>(type: "float", nullable: false),
                    Overview = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DatabaseSavingType = table.Column<int>(type: "int", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVShows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    DatabaseSavingType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBooks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMovies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    DatabaseSavingType = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMovies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMovies_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMovies_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTVShows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TVShowId = table.Column<int>(type: "int", nullable: false),
                    DatabaseSavingType = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTVShows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTVShows_TVShows_TVShowId",
                        column: x => x.TVShowId,
                        principalTable: "TVShows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTVShows_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBooks_BookId",
                table: "UserBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBooks_UserId",
                table: "UserBooks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMovies_MovieId",
                table: "UserMovies",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMovies_UserId",
                table: "UserMovies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTVShows_TVShowId",
                table: "UserTVShows",
                column: "TVShowId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTVShows_UserId",
                table: "UserTVShows",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBooks");

            migrationBuilder.DropTable(
                name: "UserMovies");

            migrationBuilder.DropTable(
                name: "UserTVShows");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "TVShows");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
