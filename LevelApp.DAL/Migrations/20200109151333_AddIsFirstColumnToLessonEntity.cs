using Microsoft.EntityFrameworkCore.Migrations;

namespace LevelApp.DAL.Migrations
{
    public partial class AddIsFirstColumnToLessonEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFirst",
                table: "CoreLesson",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFirst",
                table: "CoreLesson");
        }
    }
}
