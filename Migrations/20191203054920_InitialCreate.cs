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
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    BedCount = table.Column<int>(nullable: false),
                    BedsFilled = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabin", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    MaxAttendance = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FacStaff",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastName = table.Column<string>(nullable: true),
                    FirstMid = table.Column<string>(nullable: true),
                    Admin = table.Column<bool>(nullable: false),
                    CabinID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacStaff", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FacStaff_Cabin_CabinID",
                        column: x => x.CabinID,
                        principalTable: "Cabin",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastName = table.Column<string>(nullable: true),
                    FirstMidName = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    GradeLevel = table.Column<int>(nullable: false),
                    priorityRemaining = table.Column<int>(nullable: false),
                    CabinID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Student_Cabin_CabinID",
                        column: x => x.CabinID,
                        principalTable: "Cabin",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventAttendance",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EventID = table.Column<int>(nullable: false),
                    StudentID = table.Column<int>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    Assigned = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventAttendance", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EventAttendance_Event_EventID",
                        column: x => x.EventID,
                        principalTable: "Event",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventAttendance_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventAttendance_EventID",
                table: "EventAttendance",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_EventAttendance_StudentID",
                table: "EventAttendance",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_FacStaff_CabinID",
                table: "FacStaff",
                column: "CabinID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_CabinID",
                table: "Student",
                column: "CabinID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventAttendance");

            migrationBuilder.DropTable(
                name: "FacStaff");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Cabin");
        }
    }
}
