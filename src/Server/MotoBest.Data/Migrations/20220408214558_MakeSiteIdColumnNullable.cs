using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoBest.Data.Migrations
{
    public partial class MakeSiteIdColumnNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_Sites_SiteId",
                table: "Adverts");

            migrationBuilder.AlterColumn<int>(
                name: "SiteId",
                table: "Adverts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_Sites_SiteId",
                table: "Adverts",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_Sites_SiteId",
                table: "Adverts");

            migrationBuilder.AlterColumn<int>(
                name: "SiteId",
                table: "Adverts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_Sites_SiteId",
                table: "Adverts",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
