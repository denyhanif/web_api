using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class add_university_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UniversityId",
                table: "TB_M_Education",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TB_M_University",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_University", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Education_UniversityId",
                table: "TB_M_Education",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Education_TB_M_University_UniversityId",
                table: "TB_M_Education",
                column: "UniversityId",
                principalTable: "TB_M_University",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Education_TB_M_University_UniversityId",
                table: "TB_M_Education");

            migrationBuilder.DropTable(
                name: "TB_M_University");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Education_UniversityId",
                table: "TB_M_Education");

            migrationBuilder.DropColumn(
                name: "UniversityId",
                table: "TB_M_Education");
        }
    }
}
