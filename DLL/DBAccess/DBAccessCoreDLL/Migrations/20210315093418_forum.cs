using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DBAccessCoreDLL.Migrations
{
    public partial class forum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ForumModule",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModuleName = table.Column<long>(type: "bigint", nullable: false),
                    Q_IsDelete = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Q_Version = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Q_Sequence = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Q_CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Q_UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Q_DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumModule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ForumTopic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModuleId = table.Column<long>(type: "bigint", nullable: false),
                    TopicName = table.Column<string>(type: "text", nullable: false),
                    Q_IsDelete = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Q_Version = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Q_Sequence = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Q_CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Q_UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Q_DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumTopic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForumTopic_ForumModule_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "ForumModule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ForumPost",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    TopicId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    RichText = table.Column<string>(type: "text", nullable: false),
                    ViewsNum = table.Column<long>(type: "bigint", nullable: false),
                    ReplyNum = table.Column<long>(type: "bigint", nullable: false),
                    Q_IsDelete = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Q_Version = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Q_Sequence = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Q_CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Q_UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Q_DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForumPost_ForumTopic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "ForumTopic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ForumReply",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    RichText = table.Column<string>(type: "text", nullable: false),
                    RespondentId = table.Column<long>(type: "bigint", nullable: true),
                    PostId = table.Column<long>(type: "bigint", nullable: false),
                    Q_IsDelete = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Q_Version = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Q_Sequence = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Q_CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Q_UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Q_DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumReply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForumReply_ForumPost_PostId",
                        column: x => x.PostId,
                        principalTable: "ForumPost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ForumReply_ForumReply_RespondentId",
                        column: x => x.RespondentId,
                        principalTable: "ForumReply",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForumPost_TopicId",
                table: "ForumPost",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_ForumReply_PostId",
                table: "ForumReply",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_ForumReply_RespondentId",
                table: "ForumReply",
                column: "RespondentId");

            migrationBuilder.CreateIndex(
                name: "IX_ForumTopic_ModuleId",
                table: "ForumTopic",
                column: "ModuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForumReply");

            migrationBuilder.DropTable(
                name: "ForumPost");

            migrationBuilder.DropTable(
                name: "ForumTopic");

            migrationBuilder.DropTable(
                name: "ForumModule");
        }
    }
}
