using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentGradesProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mst_subject",
                columns: table => new
                {
                    SubjectKey = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SubjectName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_subject", x => x.SubjectKey);
                });

            migrationBuilder.CreateTable(
                name: "mst_student",
                columns: table => new
                {
                    StudentKey = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentName = table.Column<string>(type: "TEXT", nullable: false),
                    SubjectKey = table.Column<int>(type: "INTEGER", nullable: true),
                    Grade = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_student", x => x.StudentKey);
                    table.ForeignKey(
                        name: "FK_mst_student_mst_subject_SubjectKey",
                        column: x => x.SubjectKey,
                        principalTable: "mst_subject",
                        principalColumn: "SubjectKey");
                });

            migrationBuilder.CreateIndex(
                name: "IX_mst_student_SubjectKey",
                table: "mst_student",
                column: "SubjectKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mst_student");

            migrationBuilder.DropTable(
                name: "mst_subject");
        }
    }
}
