using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DBAccessCoreDLL.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Passport = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false, defaultValue: "Account"),
                    Avatar = table.Column<string>(type: "text", nullable: true, defaultValue: ""),
                    Introduction = table.Column<string>(type: "text", nullable: true, defaultValue: ""),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Sex = table.Column<int>(type: "integer", nullable: true),
                    country = table.Column<string>(type: "text", nullable: true),
                    PhoneAreaCode = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Q_IsDelete = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Q_Version = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Q_Sequence = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Q_CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Q_UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Q_DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleName = table.Column<string>(type: "text", nullable: true),
                    DisplayName = table.Column<string>(type: "text", nullable: true),
                    Organization = table.Column<string>(type: "text", nullable: true, defaultValue: "Default"),
                    Descrption = table.Column<string>(type: "text", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    Q_IsDelete = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Q_Version = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Q_Sequence = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Q_CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Q_UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Q_DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role_Role_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoutePage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    HierarchyPath = table.Column<string>(type: "text", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    Component = table.Column<string>(type: "text", nullable: false),
                    RouteName = table.Column<string>(type: "text", nullable: false),
                    ActiveMenu = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    Affix = table.Column<bool>(type: "boolean", nullable: false),
                    NoCache = table.Column<bool>(type: "boolean", nullable: false),
                    AlwaysShow = table.Column<bool>(type: "boolean", nullable: false),
                    Hidden = table.Column<bool>(type: "boolean", nullable: false),
                    Q_IsDelete = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Q_Version = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Q_Sequence = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Q_CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Q_UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Q_DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutePage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountIdentityAuth",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    IdentityType = table.Column<int>(type: "integer", nullable: false),
                    Identifier = table.Column<string>(type: "text", nullable: false),
                    bVerifier = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    AccessToken = table.Column<string>(type: "text", nullable: true),
                    Q_IsDelete = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Q_Version = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Q_Sequence = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Q_CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Q_UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Q_DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountIdentityAuth", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountIdentityAuth_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    Q_IsDelete = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Q_Version = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Q_Sequence = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Q_CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Q_UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Q_DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountRole_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoutePageRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoutePageId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    Q_IsDelete = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Q_Version = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Q_Sequence = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Q_CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Q_UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Q_DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutePageRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoutePageRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoutePageRole_RoutePage_RoutePageId",
                        column: x => x.RoutePageId,
                        principalTable: "RoutePage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "DisplayName", "Email", "Passport", "Password", "Phone", "PhoneAreaCode", "Sex", "Username", "country" },
                values: new object[,]
                {
                    { 1L, "Admin", "875191946@qq.com", "Passport_Admin", "1qaz@WSX", "18412345678", "86", null, "UserName_Admin", null },
                    { 2L, "Developer", "linqing@vip.qq.com", "Passport_Developer", "1qaz@WSX", "13712345678", "86", null, "UserName_Developer", null },
                    { 3L, "Guest", null, "Passport_Guest", "1qaz@WSX", null, null, null, "UserName_Guest", null }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Descrption", "DisplayName", "ParentId", "RoleName" },
                values: new object[,]
                {
                    { 1L, "系统管理员", "系统管理员", null, "admin" },
                    { 2L, "开发程序员", "开发程序员", null, "developer" },
                    { 3L, "编辑人员", "编辑人员", null, "editor" },
                    { 4L, "访客", "访客", null, "guest" }
                });

            migrationBuilder.InsertData(
                table: "RoutePage",
                columns: new[] { "Id", "ActiveMenu", "Affix", "AlwaysShow", "Component", "Hidden", "HierarchyPath", "Icon", "NoCache", "ParentId", "Path", "RouteName", "Title" },
                values: new object[,]
                {
                    { 10056L, "", false, false, "ErrorPages", false, "10056", "404", false, null, "/error", "ErrorPages", "errorPages" },
                    { 10055L, "", false, false, "Tab", false, "10054.10055", "tab", false, 10054L, "index", "Tab", "tab" },
                    { 10054L, "", false, false, "", false, "10054", "", false, null, "/tab", "", "" },
                    { 10053L, "", false, false, "ArticleList", false, "10050.10053", "list", false, 10050L, "list", "ArticleList", "articleList" },
                    { 10052L, "/example/list", false, false, "EditArticle", true, "10050.10052", "", true, 10050L, "edit/:id(d+)", "EditArticle", "editArticle" },
                    { 10051L, "", false, false, "CreateArticle", false, "10050.10051", "edit", false, 10050L, "create", "CreateArticle", "createArticle" },
                    { 10050L, "", false, false, "Example", false, "10050", "example", false, null, "/example", "Example", "example" },
                    { 10049L, "", false, false, "ComplexTable", false, "10045.10049", "", false, 10045L, "complex-table", "ComplexTable", "complexTable" },
                    { 10046L, "", false, false, "DynamicTable", false, "10045.10046", "", false, 10045L, "dynamic-table", "DynamicTable", "dynamicTable" },
                    { 10047L, "", false, false, "DraggableTable", false, "10045.10047", "", false, 10045L, "draggable-table", "DraggableTable", "draggableTable" },
                    { 10057L, "", false, false, "Page401", false, "10056.10057", "", true, 10056L, "401", "Page401", "page401" },
                    { 10045L, "", false, false, "Table", false, "10045", "table", false, null, "/table", "Table", "table" },
                    { 10044L, "", false, false, "Menu2", false, "10037.10044", "", false, 10037L, "menu2", "Menu2", "menu2" },
                    { 10043L, "", false, false, "Menu1-3", false, "10037.10038.10043", "", false, 10038L, "menu1-3", "Menu1-3", "menu1-3" },
                    { 10042L, "", false, false, "Menu1-2-2", false, "10037.10038.10040.10042", "", false, 10040L, "menu1-2-2", "Menu1-2-2", "menu1-2-2" },
                    { 10041L, "", false, false, "Menu1-2-1", false, "10037.10038.10040.10041", "", false, 10040L, "menu1-2-1", "Menu1-2-1", "menu1-2-1" },
                    { 10048L, "", false, false, "InlineEditTable", false, "10045.10048", "", false, 10045L, "inline-edit-table", "InlineEditTable", "inlineEditTable" },
                    { 10058L, "", false, false, "Page404", false, "10056.10058", "", true, 10056L, "404", "Page404", "page404" },
                    { 10061L, "", false, false, "Excel", false, "10061", "excel", false, null, "/excel", "Excel", "excel" },
                    { 10060L, "", false, false, "ErrorLog", false, "10059.10060", "bug", false, 10059L, "log", "ErrorLog", "errorLog" },
                    { 10076L, "", false, false, "I18n", false, "10075.10076", "international", false, 10075L, "index", "I18n", "i18n" },
                    { 10075L, "", false, false, "", false, "10075", "", false, null, "/i18n", "", "" },
                    { 10074L, "", false, false, "Clipboard", false, "10073.10074", "clipboard", false, 10073L, "index", "Clipboard", "clipboard" },
                    { 10073L, "", false, false, "", false, "10073", "", false, null, "/clipboard", "", "" },
                    { 10072L, "", false, false, "Theme", false, "10071.10072", "theme", false, 10071L, "index", "Theme", "theme" },
                    { 10071L, "", false, false, "", false, "10071", "", false, null, "/theme", "", "" },
                    { 10070L, "", false, false, "", true, "10070", "", false, null, "/pdf-download-example", "", "" },
                    { 10059L, "", false, false, "", false, "10059", "", false, null, "/error-log", "", "" },
                    { 10069L, "", false, false, "PDF", false, "10068.10069", "pdf", false, 10068L, "index", "PDF", "pdf" },
                    { 10067L, "", false, false, "ExportZip", false, "10066.10067", "", false, 10066L, "download", "ExportZip", "exportZip" },
                    { 10066L, "", false, true, "", false, "10066", "zip", false, null, "/zip", "", "zip" },
                    { 10065L, "", false, false, "UploadExcel", false, "10061.10065", "", false, 10061L, "upload-excel", "UploadExcel", "uploadExcel" },
                    { 10064L, "", false, false, "MergeHeader", false, "10061.10064", "", false, 10061L, "export-merge-header", "MergeHeader", "mergeHeader" },
                    { 10063L, "", false, false, "SelectExcel", false, "10061.10063", "", false, 10061L, "export-selected-excel", "SelectExcel", "selectExcel" },
                    { 10062L, "", false, false, "ExportExcel", false, "10061.10062", "", false, 10061L, "export-excel", "ExportExcel", "exportExcel" },
                    { 10040L, "", false, false, "Menu1-2", false, "10037.10038.10040", "", false, 10038L, "menu1-2", "Menu1-2", "menu1-2" },
                    { 10068L, "", false, false, "", false, "10068", "", false, null, "/pdf", "", "" },
                    { 10039L, "", false, false, "Menu1-1", false, "10037.10038.10039", "", false, 10038L, "menu1-1", "Menu1-1", "menu1-1" },
                    { 10035L, "", false, false, "LineChartDemo", false, "10033.10035", "", true, 10033L, "line-chart", "LineChartDemo", "lineChart" },
                    { 10037L, "", false, false, "Nested", false, "10037", "nested", false, null, "/nested", "Nested", "nested" },
                    { 10015L, "", false, false, "RolePermission", false, "10012.10015", "", false, 10012L, "role", "RolePermission", "rolePermission" },
                    { 10014L, "", false, false, "DirectivePermission", false, "10012.10014", "", false, 10012L, "directive", "DirectivePermission", "directivePermission" },
                    { 10013L, "", false, false, "PagePermission", false, "10012.10013", "", false, 10012L, "page", "PagePermission", "pagePermission" },
                    { 10012L, "", false, true, "", false, "10012", "lock", false, null, "/permission", "", "permission" },
                    { 10011L, "", false, false, "Guide", false, "10010.10011", "guide", true, 10010L, "index", "Guide", "guide" },
                    { 10010L, "", false, false, "", false, "10010", "", false, null, "/guide", "", "" },
                    { 10009L, "", true, false, "Documentation", false, "10008.10009", "documentation", false, 10008L, "index", "Documentation", "documentation" },
                    { 10016L, "", false, false, "", false, "10016", "", false, null, "/icon", "", "" },
                    { 10008L, "", false, false, "", false, "10008", "", false, null, "/documentation", "", "" },
                    { 10006L, "", false, false, "", false, "10006", "", false, null, "", "", "" },
                    { 10005L, "", false, false, "", true, "10005", "", false, null, "/401", "", "" },
                    { 10004L, "", false, false, "", true, "10004", "", false, null, "/404", "", "" },
                    { 10003L, "", false, false, "", true, "10003", "", false, null, "/auth-redirect", "", "" },
                    { 10002L, "", false, false, "", true, "10002", "", false, null, "/login", "", "" },
                    { 10001L, "", false, false, "", false, "10000.10001", "", false, 10000L, "/redirect/:path(.*)", "", "" },
                    { 10000L, "", false, false, "", true, "10000", "", false, null, "/redirect", "", "" },
                    { 10007L, "", true, false, "Dashboard", false, "10006.10007", "dashboard", false, 10006L, "dashboard", "Dashboard", "dashboard" },
                    { 10017L, "", false, false, "Icons", false, "10016.10017", "icon", true, 10016L, "index", "Icons", "icons" },
                    { 10018L, "", false, false, "ComponentDemo", false, "10018", "component", false, null, "/components", "ComponentDemo", "components" },
                    { 10019L, "", false, false, "TinymceDemo", false, "10018.10019", "", false, 10018L, "tinymce", "TinymceDemo", "tinymce" },
                    { 10036L, "", false, false, "MixedChartDemo", false, "10033.10036", "", true, 10033L, "mixedchart", "MixedChartDemo", "mixedChart" },
                    { 10077L, "", false, false, "", false, "10077", "link", false, null, "https://github.com/Armour/vue-typescript-admin-template", "", "externalLink" },
                    { 10034L, "", false, false, "BarChartDemo", false, "10033.10034", "", true, 10033L, "bar-chart", "BarChartDemo", "barChart" },
                    { 10033L, "", false, false, "Charts", false, "10033", "chart", false, null, "/charts", "Charts", "charts" },
                    { 10032L, "", false, false, "DraggableKanbanDemo", false, "10018.10032", "", false, 10018L, "draggable-kanban", "DraggableKanbanDemo", "draggableKanban" },
                    { 10031L, "", false, false, "DraggableListDemo", false, "10018.10031", "", false, 10018L, "draggable-list", "DraggableListDemo", "draggableList" },
                    { 10030L, "", false, false, "DraggableSelectDemo", false, "10018.10030", "", false, 10018L, "draggable-select", "DraggableSelectDemo", "draggableSelect" },
                    { 10029L, "", false, false, "DraggableDialogDemo", false, "10018.10029", "", false, 10018L, "draggable-dialog", "DraggableDialogDemo", "draggableDialog" },
                    { 10028L, "", false, false, "BackToTopDemo", false, "10018.10028", "", false, 10018L, "back-to-top", "BackToTopDemo", "backToTop" },
                    { 10027L, "", false, false, "ComponentMixinDemo", false, "10018.10027", "", false, 10018L, "mixin", "ComponentMixinDemo", "componentMixin" },
                    { 10026L, "", false, false, "CountToDemo", false, "10018.10026", "", false, 10018L, "count-to", "CountToDemo", "countTo" },
                    { 10025L, "", false, false, "StickyDemo", false, "10018.10025", "", false, 10018L, "sticky", "StickyDemo", "sticky" },
                    { 10024L, "", false, false, "DropzoneDemo", false, "10018.10024", "", false, 10018L, "dropzone", "DropzoneDemo", "dropzone" },
                    { 10023L, "", false, false, "AvatarUploadDemo", false, "10018.10023", "", false, 10018L, "avatar-upload", "AvatarUploadDemo", "avatarUpload" },
                    { 10022L, "", false, false, "SplitPaneDemo", false, "10018.10022", "", false, 10018L, "split-pane", "SplitPaneDemo", "splitPane" },
                    { 10021L, "", false, false, "JsonEditorDemo", false, "10018.10021", "", false, 10018L, "json-editor", "JsonEditorDemo", "jsonEditor" },
                    { 10020L, "", false, false, "MarkdownDemo", false, "10018.10020", "", false, 10018L, "markdown", "MarkdownDemo", "markdown" },
                    { 10038L, "", false, false, "Menu1", false, "10037.10038", "", false, 10037L, "menu1", "Menu1", "menu1" },
                    { 10078L, "", false, false, "", true, "10078", "", false, null, "*", "", "" }
                });

            migrationBuilder.InsertData(
                table: "AccountRole",
                columns: new[] { "Id", "AccountId", "RoleId" },
                values: new object[,]
                {
                    { 1L, 1L, 1L },
                    { 2L, 2L, 2L },
                    { 4L, 2L, 3L },
                    { 3L, 3L, 4L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_Email",
                table: "Account",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_Passport",
                table: "Account",
                column: "Passport",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_Phone_PhoneAreaCode",
                table: "Account",
                columns: new[] { "Phone", "PhoneAreaCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_Username",
                table: "Account",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountIdentityAuth_AccountId",
                table: "AccountIdentityAuth",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountIdentityAuth_IdentityType_AccountId",
                table: "AccountIdentityAuth",
                columns: new[] { "IdentityType", "AccountId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountRole_AccountId",
                table: "AccountRole",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRole_RoleId",
                table: "AccountRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_ParentId",
                table: "Role",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_RoutePageRole_RoleId",
                table: "RoutePageRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoutePageRole_RoutePageId",
                table: "RoutePageRole",
                column: "RoutePageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountIdentityAuth");

            migrationBuilder.DropTable(
                name: "AccountRole");

            migrationBuilder.DropTable(
                name: "RoutePageRole");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "RoutePage");
        }
    }
}
