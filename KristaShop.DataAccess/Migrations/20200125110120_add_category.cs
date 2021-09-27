using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KristaShop.DataAccess.Migrations
{
    public partial class add_category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dict_category",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    is_visible = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dict_category", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dict_category");
        }
    }
}
