using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace disasterApi.Infra.Migrations
{
    /// <inheritdoc />
    public partial class fixlaongtitudetyporegiontable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "Regions",
                newName: "Longtitude");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longtitude",
                table: "Regions",
                newName: "Longitude");
        }
    }
}
