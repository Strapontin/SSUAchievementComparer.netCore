using Microsoft.EntityFrameworkCore.Migrations;

namespace SSUAchievementComparer.Data.Migrations
{
    public partial class changestogametype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameName",
                table: "GameDetailsDb");

            migrationBuilder.AddColumn<int>(
                name: "appid",
                table: "GameDetailsDb",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "GameDetailsDb",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "appid",
                table: "GameDetailsDb");

            migrationBuilder.DropColumn(
                name: "name",
                table: "GameDetailsDb");

            migrationBuilder.AddColumn<string>(
                name: "GameName",
                table: "GameDetailsDb",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
