using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoBest.Data.Migrations
{
    public partial class RemoveMainImageUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImageUrl",
                table: "Adverts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainImageUrl",
                table: "Adverts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
