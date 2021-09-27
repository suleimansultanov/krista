using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KristaShop.DataAccess.Migrations
{
    public partial class add_feedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "feedbacks",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    person = table.Column<string>(maxLength: 64, nullable: false),
                    phone = table.Column<string>(maxLength: 64, nullable: false),
                    message = table.Column<string>(nullable: true),
                    email = table.Column<string>(maxLength: 64, nullable: true),
                    viewed = table.Column<bool>(nullable: false),
                    record_time_stamp = table.Column<DateTime>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false),
                    view_time_stamp = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feedbacks", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "feedbacks");
        }
    }
}
