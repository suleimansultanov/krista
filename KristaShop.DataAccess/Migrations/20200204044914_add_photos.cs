using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KristaShop.DataAccess.Migrations
{
    public partial class add_photos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "nom_photos",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    nom_id = table.Column<Guid>(nullable: false),
                    photo_path = table.Column<string>(nullable: true),
                    color_id = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nom_photos", x => x.id);
                    table.ForeignKey(
                        name: "FK_nom_photos_nomenclatures_nom_id",
                        column: x => x.nom_id,
                        principalTable: "nomenclatures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_nom_photos_nom_id",
                table: "nom_photos",
                column: "nom_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "nom_photos");
        }
    }
}
