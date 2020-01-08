using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LevelApp.DAL.Migrations
{
    public partial class AddAppUserCourseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoreAppUserCourse",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false),
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
                    table.PrimaryKey("PK_CoreAppUserCourse", x => new { x.UserId, x.CourseId });
                    table.UniqueConstraint("AK_CoreAppUserCourse_CourseId_Id_UserId", x => new { x.Id, x.CourseId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CoreAppUserCourse_CoreCourse_CourseId",
                        column: x => x.CourseId,
                        principalTable: "CoreCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoreAppUserCourse_CoreAppUser_UserId",
                        column: x => x.UserId,
                        principalTable: "CoreAppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoreAppUserCourse");
        }
    }
}
