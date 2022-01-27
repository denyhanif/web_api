using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class add_attribute_account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Education_TB_M_University_UniversityId",
                table: "TB_M_Education");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Education_UniversityId",
                table: "TB_M_Education");

            migrationBuilder.DropColumn(
                name: "University",
                table: "TB_M_Profiling");

            migrationBuilder.DropColumn(
                name: "UniversityId",
                table: "TB_M_Education");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredToken",
                table: "TB_M_Account",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "OTP",
                table: "TB_M_Account",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isUsed",
                table: "TB_M_Account",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Education_University_id",
                table: "TB_M_Education",
                column: "University_id");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Education_TB_M_University_University_id",
                table: "TB_M_Education",
                column: "University_id",
                principalTable: "TB_M_University",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Education_TB_M_University_University_id",
                table: "TB_M_Education");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Education_University_id",
                table: "TB_M_Education");

            migrationBuilder.DropColumn(
                name: "ExpiredToken",
                table: "TB_M_Account");

            migrationBuilder.DropColumn(
                name: "OTP",
                table: "TB_M_Account");

            migrationBuilder.DropColumn(
                name: "isUsed",
                table: "TB_M_Account");

            migrationBuilder.AddColumn<int>(
                name: "University",
                table: "TB_M_Profiling",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniversityId",
                table: "TB_M_Education",
                type: "int",
                nullable: true);

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
    }
}
