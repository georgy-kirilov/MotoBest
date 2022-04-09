using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoBest.Data.Migrations
{
    public partial class AddModifiedOnColumnToAdvertsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Adverts",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Adverts");
        }
    }
}
