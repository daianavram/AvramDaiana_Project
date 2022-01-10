using Microsoft.EntityFrameworkCore.Migrations;

namespace AvramDaiana_FacultyProject.Migrations
{
    public partial class ExtendedModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfessorSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Course",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_SubjectID",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "ProfessorID",
                table: "Subject");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course",
                table: "Course",
                columns: new[] { "SubjectID", "ProfessorID" });

            migrationBuilder.CreateIndex(
                name: "IX_Course_ProfessorID",
                table: "Course",
                column: "ProfessorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Course",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_ProfessorID",
                table: "Course");

            migrationBuilder.AddColumn<int>(
                name: "ProfessorID",
                table: "Subject",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course",
                table: "Course",
                columns: new[] { "ProfessorID", "SubjectID" });

            migrationBuilder.CreateTable(
                name: "ProfessorSubject",
                columns: table => new
                {
                    ProfessorsID = table.Column<int>(type: "int", nullable: false),
                    SubjectsSubjectID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorSubject", x => new { x.ProfessorsID, x.SubjectsSubjectID });
                    table.ForeignKey(
                        name: "FK_ProfessorSubject_Professor_ProfessorsID",
                        column: x => x.ProfessorsID,
                        principalTable: "Professor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessorSubject_Subject_SubjectsSubjectID",
                        column: x => x.SubjectsSubjectID,
                        principalTable: "Subject",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Course_SubjectID",
                table: "Course",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorSubject_SubjectsSubjectID",
                table: "ProfessorSubject",
                column: "SubjectsSubjectID");
        }
    }
}
