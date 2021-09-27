using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KristaShop.DataAccess.Migrations
{
    public partial class add_prod_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "nom_prod_price",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    product_id = table.Column<Guid>(nullable: false),
                    nom_id = table.Column<Guid>(nullable: false),
                    price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nom_prod_price", x => x.id);
                    table.ForeignKey(
                        name: "FK_nom_prod_price_nomenclatures_nom_id",
                        column: x => x.nom_id,
                        principalTable: "nomenclatures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "not_visible_prod_ctgrs",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    product_id = table.Column<Guid>(nullable: false),
                    nom_id = table.Column<Guid>(nullable: false),
                    category_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_not_visible_prod_ctgrs", x => x.id);
                    table.ForeignKey(
                        name: "FK_not_visible_prod_ctgrs_dict_category_category_id",
                        column: x => x.category_id,
                        principalTable: "dict_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_not_visible_prod_ctgrs_nomenclatures_nom_id",
                        column: x => x.nom_id,
                        principalTable: "nomenclatures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "not_visible_prod_ctlgs",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    product_id = table.Column<Guid>(nullable: false),
                    nom_id = table.Column<Guid>(nullable: false),
                    catalog_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_not_visible_prod_ctlgs", x => x.id);
                    table.ForeignKey(
                        name: "FK_not_visible_prod_ctlgs_catalogs_catalog_id",
                        column: x => x.catalog_id,
                        principalTable: "catalogs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_not_visible_prod_ctlgs_nomenclatures_nom_id",
                        column: x => x.nom_id,
                        principalTable: "nomenclatures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_nom_prod_price_nom_id",
                table: "nom_prod_price",
                column: "nom_id");

            migrationBuilder.CreateIndex(
                name: "IX_not_visible_prod_ctgrs_category_id",
                table: "not_visible_prod_ctgrs",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_not_visible_prod_ctgrs_nom_id",
                table: "not_visible_prod_ctgrs",
                column: "nom_id");

            migrationBuilder.CreateIndex(
                name: "IX_not_visible_prod_ctlgs_catalog_id",
                table: "not_visible_prod_ctlgs",
                column: "catalog_id");

            migrationBuilder.CreateIndex(
                name: "IX_not_visible_prod_ctlgs_nom_id",
                table: "not_visible_prod_ctlgs",
                column: "nom_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "nom_prod_price");

            migrationBuilder.DropTable(
                name: "not_visible_prod_ctgrs");

            migrationBuilder.DropTable(
                name: "not_visible_prod_ctlgs");
        }
    }
}
