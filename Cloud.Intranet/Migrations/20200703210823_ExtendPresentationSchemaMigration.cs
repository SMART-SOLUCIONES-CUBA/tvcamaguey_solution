using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Migrations
{
    public partial class ExtendPresentationSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "about",
                table: "presentations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "about",
                table: "presentations");
        }
    }
}
