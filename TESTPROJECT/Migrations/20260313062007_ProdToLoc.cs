using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TESTPROJECT.Migrations
{
    /// <inheritdoc />
    public partial class ProdToLoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductsToLocations_LocationId",
                table: "ProductsToLocations",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsToLocations_ProductId",
                table: "ProductsToLocations",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsToLocations_Locations_LocationId",
                table: "ProductsToLocations",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsToLocations_Products_ProductId",
                table: "ProductsToLocations",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsToLocations_Locations_LocationId",
                table: "ProductsToLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsToLocations_Products_ProductId",
                table: "ProductsToLocations");

            migrationBuilder.DropIndex(
                name: "IX_ProductsToLocations_LocationId",
                table: "ProductsToLocations");

            migrationBuilder.DropIndex(
                name: "IX_ProductsToLocations_ProductId",
                table: "ProductsToLocations");
        }
    }
}
