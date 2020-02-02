using Microsoft.EntityFrameworkCore.Migrations;

namespace LevelApp.DAL.Migrations
{
    public partial class ChangeUserNameToDisplayNameField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "CoreAppUser");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "CoreAppUser",
                newName: "DisplayName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "CoreAppUser",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "CoreAppUser",
                maxLength: 30,
                nullable: true);
        }
    }
}
