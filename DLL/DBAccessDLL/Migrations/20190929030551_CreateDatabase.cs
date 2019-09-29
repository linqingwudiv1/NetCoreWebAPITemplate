using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DBAccessDLL.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Qing_IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    Qing_Version = table.Column<long>(nullable: false, defaultValue: 0L)
                        .Annotation("Sqlite:Autoincrement", true),
                    Qing_Sequence = table.Column<long>(nullable: false, defaultValue: 0L)
                        .Annotation("Sqlite:Autoincrement", true),
                    Qing_CreateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Qing_UpdateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Qing_DeleteTime = table.Column<DateTime>(nullable: true),
                    username = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    avatar = table.Column<string>(nullable: true),
                    introduction = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    sex = table.Column<int>(nullable: false),
                    phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
