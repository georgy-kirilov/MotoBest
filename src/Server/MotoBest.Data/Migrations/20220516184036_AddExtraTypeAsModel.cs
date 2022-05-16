using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoBest.Data.Migrations
{
    public partial class AddExtraTypeAsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Extras",
                newName: "TypeId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Extras",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "ExtraTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Extras_Name",
                table: "Extras",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Extras_TypeId",
                table: "Extras",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraTypes_Name",
                table: "ExtraTypes",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Extras_ExtraTypes_TypeId",
                table: "Extras",
                column: "TypeId",
                principalTable: "ExtraTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Extras_ExtraTypes_TypeId",
                table: "Extras");

            migrationBuilder.DropTable(
                name: "ExtraTypes");

            migrationBuilder.DropIndex(
                name: "IX_Extras_Name",
                table: "Extras");

            migrationBuilder.DropIndex(
                name: "IX_Extras_TypeId",
                table: "Extras");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Extras",
                newName: "Type");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Extras",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
