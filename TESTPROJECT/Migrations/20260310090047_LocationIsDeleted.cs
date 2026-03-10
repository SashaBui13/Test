using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TESTPROJECT.Migrations
{
    /// <inheritdoc />
    public partial class LocationIsDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocationIsDeleted",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationIsDeleted",
                table: "Locations");
        }
    }
}
