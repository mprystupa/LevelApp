using Microsoft.EntityFrameworkCore.Migrations;

namespace LevelApp.DAL.Migrations
{
    public partial class AddAuthorIdInLesson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "CoreLesson",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CoreLesson_AuthorId",
                table: "CoreLesson",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoreLesson_CoreAppUser_AuthorId",
                table: "CoreLesson",
                column: "AuthorId",
                principalTable: "CoreAppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoreLesson_CoreAppUser_AuthorId",
                table: "CoreLesson");

            migrationBuilder.DropIndex(
                name: "IX_CoreLesson_AuthorId",
                table: "CoreLesson");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "CoreLesson");
        }
    }
}
