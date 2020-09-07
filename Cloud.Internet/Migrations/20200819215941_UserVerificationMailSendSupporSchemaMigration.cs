using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Migrations
{
    public partial class UserVerificationMailSendSupporSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userverificationmailsendorders",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    userid = table.Column<string>(nullable: false),
                    token = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_userverificationmailsendorders", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userverificationmailsendorders");
        }
    }
}
