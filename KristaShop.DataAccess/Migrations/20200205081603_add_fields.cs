using Microsoft.EntityFrameworkCore.Migrations;

namespace KristaShop.DataAccess.Migrations
{
    public partial class add_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "order",
                table: "nom_catalog",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "is_discount",
                table: "catalogs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_visible",
                table: "catalogs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "order",
                table: "catalogs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order",
                table: "nom_catalog");

            migrationBuilder.DropColumn(
                name: "is_discount",
                table: "catalogs");

            migrationBuilder.DropColumn(
                name: "is_visible",
                table: "catalogs");

            migrationBuilder.DropColumn(
                name: "order",
                table: "catalogs");
        }
    }
}
