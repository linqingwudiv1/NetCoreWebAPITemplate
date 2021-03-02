using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DBAccessCoreDLL.Migrations
{
    public partial class Initial : Migration
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
                name: "AppInfo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppName = table.Column<string>(type: "text", nullable: false),
                    AppVersion = table.Column<string>(type: "text", nullable: false),
                    bLatest = table.Column<bool>(type: "boolean", nullable: false),
                    bForceUpdate = table.Column<bool>(type: "boolean", nullable: false),
                    Q_IsDelete = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Q_Version = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Q_Sequence = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Q_CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Q_UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Q_DeleteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInfo", x => x.Id);
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
                    HierarchyPath = table.Column<string>(type: "text", nullable: true),
                    Platform = table.Column<string>(type: "text", nullable: false, defaultValue: ""),
                    GroupName = table.Column<string>(type: "text", nullable: false, defaultValue: ""),
                    Path = table.Column<string>(type: "text", nullable: true),
                    Component = table.Column<string>(type: "text", nullable: true),
                    RouteName = table.Column<string>(type: "text", nullable: true),
                    ActiveMenu = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    Redirect = table.Column<string>(type: "text", nullable: true),
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
                    { 1L, "Admin", "875191946@qq.com", "passport_admin", "1qaz@WSX", "18412345678", "86", null, "UserName_Admin", null },
                    { 2L, "Developer", "linqing@vip.qq.com", "passport_developer", "1qaz@WSX", "13712345678", "86", null, "UserName_Developer", null },
                    { 3L, "Guest", "aa875191946@vip.qq.com", "passport_guest", "1qaz@WSX", null, null, null, "UserName_Guest", null }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Descrption", "DisplayName", "ParentId", "RoleName" },
                values: new object[,]
                {
                    { 1L, "系统管理人员", "系统管理员", null, "admin" },
                    { 2L, "开发程序人员", "开发程序员", null, "developer" },
                    { 3L, "编辑测试人员", "系统测试员", null, "editor" },
                    { 4L, "访客", "访客", null, "guest" }
                });

            migrationBuilder.InsertData(
                table: "RoutePage",
                columns: new[] { "Id", "ActiveMenu", "Affix", "AlwaysShow", "Component", "Hidden", "HierarchyPath", "Icon", "NoCache", "ParentId", "Path", "Redirect", "RouteName", "Title" },
                values: new object[,]
                {
                    { 10047L, "", false, false, "Layout", false, "10047", "", false, null, "/error-log", "noredirect", null, "" },
                    { 10046L, "", false, false, "views/error-page/404.vue", false, "10044.10046", "", true, 10044L, "404", null, "Page404", "page404" },
                    { 10045L, "", false, false, "views/error-page/401.vue", false, "10044.10045", "", true, 10044L, "401", null, "Page401", "page401" },
                    { 10044L, "", false, false, "Layout", false, "10044", "404", false, null, "/error", "noredirect", null, "errorPages" },
                    { 10043L, "", false, false, "views/tab/index.vue", false, "10042.10043", "tab", false, 10042L, "index", null, "Tab", "tab" },
                    { 10042L, "", false, false, "Layout", false, "10042", "", false, null, "/tab", null, null, "" },
                    { 10038L, "", false, false, "Layout", false, "10038", "example", false, null, "/example", "/example/list", null, "example" },
                    { 10040L, "/example/list", false, false, "views/example/edit.vue", true, "10038.10040", "", true, 10038L, "edit/:id(\\d+)", null, "EditArticle", "editArticle" },
                    { 10039L, "", false, false, "views/example/create.vue", false, "10038.10039", "edit", false, 10038L, "create", null, "CreateArticle", "createArticle" },
                    { 10048L, "", false, false, "views/error-log/index.vue", false, "10047.10048", "bug", false, 10047L, "log", null, "ErrorLog", "errorLog" },
                    { 10037L, "", false, false, "views/table/complex-table.vue", false, "10033.10037", "", false, 10033L, "complex-table", null, "ComplexTable", "complexTable" },
                    { 10036L, "", false, false, "views/table/inline-edit-table.vue", false, "10033.10036", "", false, 10033L, "inline-edit-table", null, "InlineEditTable", "inlineEditTable" },
                    { 10035L, "", false, false, "views/table/draggable-table.vue", false, "10033.10035", "", false, 10033L, "draggable-table", null, "DraggableTable", "draggableTable" },
                    { 10041L, "", false, false, "views/example/list.vue", false, "10038.10041", "list", false, 10038L, "list", null, "ArticleList", "articleList" },
                    { 10049L, "", false, false, "Layout", false, "10049", "excel", false, null, "/excel", "/excel/export-excel", null, "excel" },
                    { 10052L, "", false, false, "views/excel/merge-header.vue", false, "10049.10052", "", false, 10049L, "export-merge-header", null, "MergeHeader", "mergeHeader" },
                    { 10051L, "", false, false, "views/excel/select-excel.vue", false, "10049.10051", "", false, 10049L, "export-selected-excel", null, "SelectExcel", "selectExcel" },
                    { 10034L, "", false, false, "views/table/dynamic-table/index.vue", false, "10033.10034", "", false, 10033L, "dynamic-table", null, "DynamicTable", "dynamicTable" },
                    { 10053L, "", false, false, "views/excel/upload-excel.vue", false, "10049.10053", "", false, 10049L, "upload-excel", null, "UploadExcel", "uploadExcel" },
                    { 10054L, "", false, true, "Layout", false, "10054", "zip", false, null, "/zip", "/zip/download", null, "zip" },
                    { 10055L, "", false, false, "views/zip/index.vue", false, "10054.10055", "", false, 10054L, "download", null, "ExportZip", "exportZip" },
                    { 10056L, "", false, false, "Layout", false, "10056", "", false, null, "/pdf", "/pdf/index", null, "" },
                    { 10057L, "", false, false, "views/pdf/index.vue", false, "10056.10057", "pdf", false, 10056L, "index", null, "PDF", "pdf" },
                    { 10058L, "", false, false, "views/pdf/download.vue", true, "10058", "", false, null, "/pdf-download-example", null, null, "" },
                    { 10059L, "", false, false, "Layout", false, "10059", "", false, null, "/theme", "noredirect", null, "" },
                    { 10060L, "", false, false, "views/theme/index.vue", false, "10059.10060", "theme", false, 10059L, "index", null, "Theme", "theme" },
                    { 10061L, "", false, false, "Layout", false, "10061", "", false, null, "/clipboard", "noredirect", null, "" },
                    { 10062L, "", false, false, "views/clipboard/index.vue", false, "10061.10062", "clipboard", false, 10061L, "index", null, "Clipboard", "clipboard" },
                    { 10063L, "", false, false, "Layout", false, "10063", "", false, null, "/i18n", null, null, "" },
                    { 10064L, "", false, false, "views/i18n-demo/index.vue", false, "10063.10064", "international", false, 10063L, "index", null, "I18n", "i18n" },
                    { 10050L, "", false, false, "views/excel/export-excel.vue", false, "10049.10050", "", false, 10049L, "export-excel", null, "ExportExcel", "exportExcel" },
                    { 10033L, "", false, false, "Layout", false, "10033", "table", false, null, "/table", "/table/complex-table", "Table", "table" },
                    { 10029L, "", false, false, "views/nested/menu1/menu1-2/menu1-2-1/index.vue", false, "10025.10026.10028.10029", "", false, 10028L, "menu1-2-1", null, "Menu1-2-1", "menu1-2-1" },
                    { 10031L, "", false, false, "views/nested/menu1/menu1-3/index.vue", false, "10025.10026.10031", "", false, 10026L, "menu1-3", null, "Menu1-3", "menu1-3" },
                    { 10000L, "", false, true, "Layout", false, "10000", "lock", false, null, "/perm1ission", "/permission/directive", null, "permission" },
                    { 10001L, "", false, false, "views/permission/page.vue", false, "10000.10001", "", false, 10000L, "page", null, "PagePermission", "pagePermission" },
                    { 10002L, "", false, false, "views/permission/directive.vue", false, "10000.10002", "", false, 10000L, "directive", null, "DirectivePermission", "directivePermission" },
                    { 10003L, "", false, false, "views/permission/role.vue", false, "10000.10003", "", false, 10000L, "role", null, "RolePermission", "rolePermission" },
                    { 10004L, "", false, false, "Layout", false, "10004", "", false, null, "/icon", null, null, "" },
                    { 10005L, "", false, false, "views/icons/index.vue", false, "10004.10005", "icon", true, 10004L, "index", null, "Icons", "icons" },
                    { 10006L, "", false, false, "Layout", false, "10006", "component", false, null, "/components", "noRedirect", "ComponentDemo", "components" },
                    { 10007L, "", false, false, "views/components-demo/tinymce.vue", false, "10006.10007", "", false, 10006L, "tinymce", null, "TinymceDemo", "tinymce" },
                    { 10008L, "", false, false, "views/components-demo/markdown.vue", false, "10006.10008", "", false, 10006L, "markdown", null, "MarkdownDemo", "markdown" },
                    { 10009L, "", false, false, "", false, "10006.10009", "", false, 10006L, "json-editor", null, "JsonEditorDemo", "jsonEditor" },
                    { 10010L, "", false, false, "views/components-demo/split-pane.vue", false, "10006.10010", "", false, 10006L, "split-pane", null, "SplitPaneDemo", "splitPane" },
                    { 10011L, "", false, false, "views/components-demo/avatar-upload.vue", false, "10006.10011", "", false, 10006L, "avatar-upload", null, "AvatarUploadDemo", "avatarUpload" },
                    { 10012L, "", false, false, "views/components-demo/dropzone.vue", false, "10006.10012", "", false, 10006L, "dropzone", null, "DropzoneDemo", "dropzone" },
                    { 10013L, "", false, false, "views/components-demo/sticky.vue", false, "10006.10013", "", false, 10006L, "sticky", null, "StickyDemo", "sticky" },
                    { 10014L, "", false, false, "views/components-demo/count-to.vue", false, "10006.10014", "", false, 10006L, "count-to", null, "CountToDemo", "countTo" },
                    { 10015L, "", false, false, "views/components-demo/mixin.vue", false, "10006.10015", "", false, 10006L, "mixin", null, "ComponentMixinDemo", "componentMixin" },
                    { 10016L, "", false, false, "views/components-demo/back-to-top.vue", false, "10006.10016", "", false, 10006L, "back-to-top", null, "BackToTopDemo", "backToTop" },
                    { 10030L, "", false, false, "views/nested/menu1/menu1-2/menu1-2-2/index.vue", false, "10025.10026.10028.10030", "", false, 10028L, "menu1-2-2", null, "Menu1-2-2", "menu1-2-2" },
                    { 10065L, "", false, false, "", false, "10065", "link", false, null, "https://github.com/Armour/vue-typescript-admin-template", null, null, "externalLink" },
                    { 10028L, "", false, false, "views/nested/menu1/menu1-2/index.vue", false, "10025.10026.10028", "", false, 10026L, "menu1-2", "/nested/menu1/menu1-2/menu1-2-1", "Menu1-2", "menu1-2" },
                    { 10027L, "", false, false, "views/nested/menu1/menu1-1/index.vue", false, "10025.10026.10027", "", false, 10026L, "menu1-1", null, "Menu1-1", "menu1-1" },
                    { 10026L, "", false, false, "views/nested/menu1/index.vue", false, "10025.10026", "", false, 10025L, "menu1", "/nested/menu1/menu1-1", "Menu1", "menu1" },
                    { 10025L, "", false, false, "Layout", false, "10025", "nested", false, null, "/nested", "/nested/menu1", "Nested", "nested" },
                    { 10032L, "", false, false, "views/nested/menu2/index.vue", false, "10025.10032", "", false, 10025L, "menu2", null, "Menu2", "menu2" },
                    { 10024L, "", false, false, "views/charts/mixed-chart.vue", false, "10021.10024", "", true, 10021L, "mixed-chart", null, "MixedChartDemo", "mixedChart" },
                    { 10022L, "", false, false, "views/charts/bar-chart.vue", false, "10021.10022", "", true, 10021L, "bar-chart", null, "BarChartDemo", "barChart" },
                    { 10021L, "", false, false, "Layout", false, "10021", "chart", false, null, "/charts", "noredirect", "Charts", "charts" },
                    { 10020L, "", false, false, "views/components-demo/draggable-select.vue", false, "10006.10020", "", false, 10006L, "draggable-select", null, "DraggableSelectDemo", "draggableSelect" },
                    { 10019L, "", false, false, "views/components-demo/draggable-list.vue", false, "10006.10019", "", false, 10006L, "draggable-list", null, "DraggableListDemo", "draggableList" },
                    { 10018L, "", false, false, "views/components-demo/draggable-kanban.vue", false, "10006.10018", "", false, 10006L, "draggable-kanban", null, "DraggableKanbanDemo", "draggableKanban" },
                    { 10017L, "", false, false, "views/components-demo/draggable-dialog.vue", false, "10006.10017", "", false, 10006L, "draggable-dialog", null, "DraggableDialogDemo", "draggableDialog" },
                    { 10023L, "", false, false, "views/charts/line-chart.vue", false, "10021.10023", "", true, 10021L, "line-chart", null, "LineChartDemo", "lineChart" },
                    { 10066L, "", false, false, "", true, "10066", "", false, null, "*", "/404", null, "" }
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
                name: "IX_AppInfo_AppName_AppVersion",
                table: "AppInfo",
                columns: new[] { "AppName", "AppVersion" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_ParentId",
                table: "Role",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_RoutePage_HierarchyPath",
                table: "RoutePage",
                column: "HierarchyPath",
                unique: true);

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
                name: "AppInfo");

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
