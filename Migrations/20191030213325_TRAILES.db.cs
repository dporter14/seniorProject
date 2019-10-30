using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TRAILES.Migrations
{
    public partial class TRAILESdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cabin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    AddDate = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    BedCount = table.Column<int>(nullable: false),
                    BedsFilled = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabin", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cabin");
        }
    }
}
