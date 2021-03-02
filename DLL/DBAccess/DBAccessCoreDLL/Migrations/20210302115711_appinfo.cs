using Microsoft.EntityFrameworkCore.Migrations;

namespace DBAccessCoreDLL.Migrations
{
    public partial class appinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "url",
                table: "AppInfo",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "url",
                table: "AppInfo");
        }
    }
}
