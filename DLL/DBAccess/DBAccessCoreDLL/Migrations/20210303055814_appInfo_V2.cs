using Microsoft.EntityFrameworkCore.Migrations;

namespace DBAccessCoreDLL.Migrations
{
    public partial class appInfo_V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "url",
                table: "AppInfo",
                newName: "Url");

            migrationBuilder.AddColumn<bool>(
                name: "bBeta",
                table: "AppInfo",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "bEnable",
                table: "AppInfo",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bBeta",
                table: "AppInfo");

            migrationBuilder.DropColumn(
                name: "bEnable",
                table: "AppInfo");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "AppInfo",
                newName: "url");
        }
    }
}
