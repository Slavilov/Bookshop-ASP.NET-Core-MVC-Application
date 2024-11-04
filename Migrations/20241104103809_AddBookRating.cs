using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookshop_ASP.NET_Core_MVC_Application.Migrations
{
    /// <inheritdoc />
    public partial class AddBookRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RatingCount",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RatingTotal",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatingCount",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "RatingTotal",
                table: "Books");
        }
    }
}
