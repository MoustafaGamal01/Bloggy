using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggy.Migrations
{
    /// <inheritdoc />
    public partial class AddisBannedPropToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isBanned",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isBanned",
                table: "AspNetUsers");
        }
    }
}
