using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TESTPROJECT.Migrations
{
    /// <inheritdoc />
    public partial class IsDeltedToBool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "LocationIsDeleted",
                table: "Locations",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LocationIsDeleted",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
