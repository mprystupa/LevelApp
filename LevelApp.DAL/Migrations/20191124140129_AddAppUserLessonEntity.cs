using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LevelApp.DAL.Migrations
{
    public partial class AddAppUserLessonEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoreAppUserLesson",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LessonId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DateCreatedUtc = table.Column<DateTime>(nullable: true),
                    DateModifiedUtc = table.Column<DateTime>(nullable: true),
                    DateDeletedUtc = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IsFavourite = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoreAppUserLesson", x => new { x.UserId, x.LessonId });
                    table.UniqueConstraint("AK_CoreAppUserLesson_Id_LessonId_UserId", x => new { x.Id, x.LessonId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CoreAppUserLesson_CoreLesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "CoreLesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoreAppUserLesson_CoreAppUser_UserId",
                        column: x => x.UserId,
                        principalTable: "CoreAppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoreAppUserLesson_LessonId",
                table: "CoreAppUserLesson",
                column: "LessonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoreAppUserLesson");
        }
    }
}
