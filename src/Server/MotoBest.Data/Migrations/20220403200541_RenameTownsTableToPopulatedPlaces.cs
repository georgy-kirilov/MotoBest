using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoBest.Data.Migrations
{
    public partial class RenameTownsTableToPopulatedPlaces : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_Sites_SiteId",
                table: "Adverts");

            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_Towns_TownId",
                table: "Adverts");

            migrationBuilder.DropTable(
                name: "Towns");

            migrationBuilder.RenameColumn(
                name: "TownId",
                table: "Adverts",
                newName: "PopulatedPlaceId");

            migrationBuilder.RenameIndex(
                name: "IX_Adverts_TownId",
                table: "Adverts",
                newName: "IX_Adverts_PopulatedPlaceId");

            migrationBuilder.AlterColumn<int>(
                name: "SiteId",
                table: "Adverts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PopulatedPlaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopulatedPlaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PopulatedPlaces_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PopulatedPlaces_RegionId",
                table: "PopulatedPlaces",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_PopulatedPlaces_PopulatedPlaceId",
                table: "Adverts",
                column: "PopulatedPlaceId",
                principalTable: "PopulatedPlaces",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_Sites_SiteId",
                table: "Adverts",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_PopulatedPlaces_PopulatedPlaceId",
                table: "Adverts");

            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_Sites_SiteId",
                table: "Adverts");

            migrationBuilder.DropTable(
                name: "PopulatedPlaces");

            migrationBuilder.RenameColumn(
                name: "PopulatedPlaceId",
                table: "Adverts",
                newName: "TownId");

            migrationBuilder.RenameIndex(
                name: "IX_Adverts_PopulatedPlaceId",
                table: "Adverts",
                newName: "IX_Adverts_TownId");

            migrationBuilder.AlterColumn<int>(
                name: "SiteId",
                table: "Adverts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Towns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    IsVillage = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Towns_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Towns_RegionId",
                table: "Towns",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_Sites_SiteId",
                table: "Adverts",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_Towns_TownId",
                table: "Adverts",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id");
        }
    }
}
