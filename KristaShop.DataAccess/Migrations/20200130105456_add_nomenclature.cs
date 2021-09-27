using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KristaShop.DataAccess.Migrations
{
    public partial class add_nomenclature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "nomenclatures",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    articul = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    default_price = table.Column<decimal>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    youtube_link = table.Column<string>(nullable: true),
                    meta_title = table.Column<string>(nullable: true),
                    link_name = table.Column<string>(nullable: true),
                    meta_keywords = table.Column<string>(nullable: true),
                    meta_description = table.Column<string>(nullable: true),
                    is_visible = table.Column<bool>(nullable: false),
                    image_path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nomenclatures", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nom_catalog",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    nom_id = table.Column<Guid>(nullable: false),
                    catalog_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nom_catalog", x => x.id);
                    table.ForeignKey(
                        name: "FK_nom_catalog_catalogs_catalog_id",
                        column: x => x.catalog_id,
                        principalTable: "catalogs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_nom_catalog_nomenclatures_nom_id",
                        column: x => x.nom_id,
                        principalTable: "nomenclatures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "nom_category",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    nom_id = table.Column<Guid>(nullable: false),
                    category_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nom_category", x => x.id);
                    table.ForeignKey(
                        name: "FK_nom_category_dict_category_category_id",
                        column: x => x.category_id,
                        principalTable: "dict_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_nom_category_nomenclatures_nom_id",
                        column: x => x.nom_id,
                        principalTable: "nomenclatures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "visible_nom_users",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    nom_id = table.Column<Guid>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_visible_nom_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_visible_nom_users_nomenclatures_nom_id",
                        column: x => x.nom_id,
                        principalTable: "nomenclatures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_nom_catalog_catalog_id",
                table: "nom_catalog",
                column: "catalog_id");

            migrationBuilder.CreateIndex(
                name: "IX_nom_catalog_nom_id",
                table: "nom_catalog",
                column: "nom_id");

            migrationBuilder.CreateIndex(
                name: "IX_nom_category_category_id",
                table: "nom_category",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_nom_category_nom_id",
                table: "nom_category",
                column: "nom_id");

            migrationBuilder.CreateIndex(
                name: "IX_visible_nom_users_nom_id",
                table: "visible_nom_users",
                column: "nom_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "nom_catalog");

            migrationBuilder.DropTable(
                name: "nom_category");

            migrationBuilder.DropTable(
                name: "visible_nom_users");

            migrationBuilder.DropTable(
                name: "nomenclatures");
        }
    }
}
