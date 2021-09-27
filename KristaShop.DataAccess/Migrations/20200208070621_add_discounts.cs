using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KristaShop.DataAccess.Migrations
{
    public partial class add_discounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "catalog_discounts",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    catalog_id = table.Column<Guid>(nullable: false),
                    discount_price = table.Column<double>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    start_date = table.Column<DateTime>(nullable: true),
                    end_date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catalog_discounts", x => x.id);
                    table.ForeignKey(
                        name: "FK_catalog_discounts_catalogs_catalog_id",
                        column: x => x.catalog_id,
                        principalTable: "catalogs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "nom_discounts",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    nom_id = table.Column<Guid>(nullable: false),
                    discount_price = table.Column<double>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    start_date = table.Column<DateTime>(nullable: true),
                    end_date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nom_discounts", x => x.id);
                    table.ForeignKey(
                        name: "FK_nom_discounts_nomenclatures_nom_id",
                        column: x => x.nom_id,
                        principalTable: "nomenclatures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_discounts",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false),
                    discount_price = table.Column<double>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    start_date = table.Column<DateTime>(nullable: true),
                    end_date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_discounts", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_catalog_discounts_catalog_id",
                table: "catalog_discounts",
                column: "catalog_id");

            migrationBuilder.CreateIndex(
                name: "IX_nom_discounts_nom_id",
                table: "nom_discounts",
                column: "nom_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "catalog_discounts");

            migrationBuilder.DropTable(
                name: "nom_discounts");

            migrationBuilder.DropTable(
                name: "user_discounts");
        }
    }
}
