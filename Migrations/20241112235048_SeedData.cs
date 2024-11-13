using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Assesment.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "UserID", "DateOfBirth", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "johndoe@example.com", "John", "Doe" },
                    { 2, new DateTime(1992, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "janesmith@example.com", "Jane", "Smith" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostID", "Content", "DatePosted", "Title", "UserID" },
                values: new object[,]
                {
                    { 1, "This is a post about C# basics.", new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Introduction to C#", 1 },
                    { 2, "This post covers ASP.NET Core fundamentals.", new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ASP.NET Core Overview", 1 },
                    { 3, "JavaScript tips for beginners.", new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "JavaScript Tips", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "UserID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "UserID",
                keyValue: 2);
        }
    }
}
