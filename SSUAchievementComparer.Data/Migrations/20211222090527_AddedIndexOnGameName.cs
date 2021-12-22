using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SSUAchievementComparer.Data.Migrations
{
    public partial class AddedIndexOnGameName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "GameDetailsDb",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_GameDetailsDb_name",
                table: "GameDetailsDb",
                column: "name")
                .Annotation("SqlServer:Include", new[] { "appid" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GameDetailsDb_name",
                table: "GameDetailsDb");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "GameDetailsDb",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
