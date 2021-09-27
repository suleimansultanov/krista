using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KristaShop.DataAccess.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "authorization_link",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    random_code = table.Column<string>(maxLength: 64, nullable: false),
                    user_id = table.Column<Guid>(nullable: false),
                    record_time_stamp = table.Column<DateTime>(nullable: false),
                    valid_to = table.Column<DateTime>(nullable: true),
                    login_date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authorization_link", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dict_settings",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    key = table.Column<string>(maxLength: 64, nullable: false),
                    value = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dict_settings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "menu_contents",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    url = table.Column<string>(maxLength: 256, nullable: false),
                    title = table.Column<string>(maxLength: 64, nullable: false),
                    body = table.Column<string>(nullable: false),
                    layout = table.Column<string>(maxLength: 64, nullable: false),
                    meta_title = table.Column<string>(maxLength: 500, nullable: true),
                    meta_description = table.Column<string>(maxLength: 500, nullable: true),
                    meta_keywords = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu_contents", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "menu_items",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    menu_type = table.Column<int>(nullable: false),
                    title = table.Column<string>(maxLength: 64, nullable: false),
                    controller_name = table.Column<string>(maxLength: 64, nullable: false),
                    action_name = table.Column<string>(maxLength: 64, nullable: false),
                    url = table.Column<string>(maxLength: 256, nullable: true),
                    parameters = table.Column<string>(nullable: true),
                    icon = table.Column<string>(nullable: true),
                    order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu_items", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "url_access",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    url = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    acl = table.Column<int>(nullable: false),
                    access_groups_json = table.Column<string>(nullable: true),
                    denied_groups_json = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_url_access", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "authorization_link");

            migrationBuilder.DropTable(
                name: "dict_settings");

            migrationBuilder.DropTable(
                name: "menu_contents");

            migrationBuilder.DropTable(
                name: "menu_items");

            migrationBuilder.DropTable(
                name: "url_access");
        }
    }
}
