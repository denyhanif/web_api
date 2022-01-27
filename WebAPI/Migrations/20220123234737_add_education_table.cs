using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class add_education_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EducationId",
                table: "TB_M_Profiling",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TB_M_Education",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GPA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    University_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Education", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Profiling_EducationId",
                table: "TB_M_Profiling",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Profiling_TB_M_Education_EducationId",
                table: "TB_M_Profiling",
                column: "EducationId",
                principalTable: "TB_M_Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Profiling_TB_M_Education_EducationId",
                table: "TB_M_Profiling");

            migrationBuilder.DropTable(
                name: "TB_M_Education");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Profiling_EducationId",
                table: "TB_M_Profiling");

            migrationBuilder.DropColumn(
                name: "EducationId",
                table: "TB_M_Profiling");
        }
    }
}
