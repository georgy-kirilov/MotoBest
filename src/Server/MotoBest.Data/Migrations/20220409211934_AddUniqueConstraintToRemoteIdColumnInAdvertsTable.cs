using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoBest.Data.Migrations
{
    public partial class AddUniqueConstraintToRemoteIdColumnInAdvertsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RemoteId",
                table: "Adverts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Adverts_RemoteId",
                table: "Adverts",
                column: "RemoteId",
                unique: true,
                filter: "[RemoteId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Adverts_RemoteId",
                table: "Adverts");

            migrationBuilder.AlterColumn<string>(
                name: "RemoteId",
                table: "Adverts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
