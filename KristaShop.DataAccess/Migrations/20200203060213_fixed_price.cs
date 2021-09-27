using Microsoft.EntityFrameworkCore.Migrations;

namespace KristaShop.DataAccess.Migrations
{
    public partial class fixed_price : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "price",
                table: "nom_prod_price",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "nom_prod_price",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
