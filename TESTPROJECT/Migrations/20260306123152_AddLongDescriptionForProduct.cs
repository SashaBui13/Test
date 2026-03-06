using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TESTPROJECT.Migrations
{
    /// <inheritdoc />
    public partial class AddLongDescriptionForProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LongDescription",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LongDescription",
                table: "Products");
        }
    }
}
