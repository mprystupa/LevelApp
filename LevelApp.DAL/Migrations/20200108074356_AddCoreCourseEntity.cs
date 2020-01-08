using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LevelApp.DAL.Migrations
{
    public partial class AddCoreCourseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "CoreLesson",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CoreCourse",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DateCreatedUtc = table.Column<DateTime>(nullable: true),
                    DateModifiedUtc = table.Column<DateTime>(nullable: true),
                    DateDeletedUtc = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    TagList = table.Column<string>(maxLength: 1000, nullable: true),
                    AuthorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoreCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoreCourse_CoreAppUser_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "CoreAppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoreLesson_CourseId",
                table: "CoreLesson",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CoreCourse_AuthorId",
                table: "CoreCourse",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoreLesson_CoreCourse_CourseId",
                table: "CoreLesson",
                column: "CourseId",
                principalTable: "CoreCourse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoreLesson_CoreCourse_CourseId",
                table: "CoreLesson");

            migrationBuilder.DropTable(
                name: "CoreCourse");

            migrationBuilder.DropIndex(
                name: "IX_CoreLesson_CourseId",
                table: "CoreLesson");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CoreLesson");
        }
    }
}
