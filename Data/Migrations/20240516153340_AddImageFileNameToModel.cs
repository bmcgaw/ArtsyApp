using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtsyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddImageFileNameToModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "ArtPost",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "ArtPost");
        }
    }
}
