using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoBest.Data.Migrations
{
    public partial class AddUnitSuffixesToSomeColumnsInAdvertsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceBgn",
                table: "Adverts",
                newName: "PriceInBgn");

            migrationBuilder.RenameColumn(
                name: "Kilometrage",
                table: "Adverts",
                newName: "PowerInHp");

            migrationBuilder.RenameColumn(
                name: "HorsePowers",
                table: "Adverts",
                newName: "MileageInKm");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceInBgn",
                table: "Adverts",
                newName: "PriceBgn");

            migrationBuilder.RenameColumn(
                name: "PowerInHp",
                table: "Adverts",
                newName: "Kilometrage");

            migrationBuilder.RenameColumn(
                name: "MileageInKm",
                table: "Adverts",
                newName: "HorsePowers");
        }
    }
}
