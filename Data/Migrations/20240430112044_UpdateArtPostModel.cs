using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtsyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateArtPostModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "ArtPost");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "ArtPost",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "ArtPost");

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "ArtPost",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
