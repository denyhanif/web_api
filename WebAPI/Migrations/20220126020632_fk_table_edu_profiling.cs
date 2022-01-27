using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class fk_table_edu_profiling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Profiling_TB_M_Education_EducationId",
                table: "TB_M_Profiling");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Profiling_EducationId",
                table: "TB_M_Profiling");

            migrationBuilder.RenameColumn(
                name: "EducationId",
                table: "TB_M_Profiling",
                newName: "University");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Profiling_Education_id",
                table: "TB_M_Profiling",
                column: "Education_id");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Profiling_TB_M_Education_Education_id",
                table: "TB_M_Profiling",
                column: "Education_id",
                principalTable: "TB_M_Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Profiling_TB_M_Education_Education_id",
                table: "TB_M_Profiling");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Profiling_Education_id",
                table: "TB_M_Profiling");

            migrationBuilder.RenameColumn(
                name: "University",
                table: "TB_M_Profiling",
                newName: "EducationId");

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
    }
}
