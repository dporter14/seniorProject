using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TRAILES.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cabin",
                columns: table => new
                {
                    CabinId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    AddDate = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<string>(maxLength: 10, nullable: false),
                    BedCount = table.Column<int>(nullable: false),
                    BedsFilled = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabin", x => x.CabinId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FName = table.Column<string>(maxLength: 255, nullable: false),
                    LName = table.Column<string>(maxLength: 255, nullable: false),
                    Gender = table.Column<string>(maxLength: 10, nullable: false),
                    GradeLevel = table.Column<int>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false),
                    CabinId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Cabin_CabinId",
                        column: x => x.CabinId,
                        principalTable: "Cabin",
                        principalColumn: "CabinId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_CabinId",
                table: "User",
                column: "CabinId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Cabin");
        }
    }
}
