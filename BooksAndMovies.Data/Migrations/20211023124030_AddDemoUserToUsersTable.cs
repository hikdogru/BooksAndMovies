using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksAndMovies.Data.Migrations
{
    public partial class AddDemoUserToUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password" },
                values: new object[] { 10, "demouser@gmail.com", "Demo", "User", "$2a$11$Q8YBYHlGu0VYvaVeueSEwu1MFePIOkqqri/hEw50xzWePlsxXsDMa" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password" },
                values: new object[] { -1, "demouser@gmail.com", "Demo", "User", "$2a$11$fs2rMQa/T7QxIQNSPshB3.raMrnvKbTE5g0hZ/e3SqdfshAzBKVFK" });
        }
    }
}
