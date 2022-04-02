using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoBest.Data.Migrations
{
    public partial class AddIsVillageColumnToTownsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVillage",
                table: "Towns",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVillage",
                table: "Towns");
        }
    }
}
