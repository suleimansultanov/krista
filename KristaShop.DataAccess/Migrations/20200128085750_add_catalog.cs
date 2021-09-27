using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KristaShop.DataAccess.Migrations
{
    public partial class add_catalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "catalogs",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 64, nullable: false),
                    uri = table.Column<string>(maxLength: 64, nullable: false),
                    order_form = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    meta_title = table.Column<string>(nullable: true),
                    meta_keywords = table.Column<string>(nullable: true),
                    meta_description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catalogs", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "catalogs");
        }
    }
}
