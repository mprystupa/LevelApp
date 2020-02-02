using Microsoft.EntityFrameworkCore.Migrations;

namespace LevelApp.DAL.Migrations
{
    public partial class ChangeLessonAndCourseEntityAddIconUrlAndTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "CoreLesson",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TagList",
                table: "CoreLesson",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "CoreCourse",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "CoreLesson");

            migrationBuilder.DropColumn(
                name: "TagList",
                table: "CoreLesson");

            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "CoreCourse");
        }
    }
}
