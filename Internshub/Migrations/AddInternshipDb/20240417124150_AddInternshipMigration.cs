using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Internshub.Migrations.AddInternshipDb
{
    /// <inheritdoc />
    public partial class AddInternshipMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Internship",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Internship_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Internship_Skills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Internship_details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IntershipPicUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    InternshipType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternshipMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Responsibilites = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinQualification = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Internship", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Internship");
        }
    }
}
