using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Migrations
{
    public partial class IncludeNewsSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    parentid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                    table.ForeignKey(
                        name: "fk_categories_categories_parentid",
                        column: x => x.parentid,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tags", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    title = table.Column<string>(nullable: false),
                    summary = table.Column<string>(nullable: false),
                    content = table.Column<string>(nullable: false),
                    postedat = table.Column<DateTime>(nullable: false),
                    authorid = table.Column<Guid>(nullable: false),
                    categoryid = table.Column<string>(nullable: true),
                    posterid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_posts", x => x.id);
                    table.ForeignKey(
                        name: "fk_posts_employs_authorid",
                        column: x => x.authorid,
                        principalTable: "employs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_posts_categories_categoryid",
                        column: x => x.categoryid,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_posts_images_posterid",
                        column: x => x.posterid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "posttagbridges",
                columns: table => new
                {
                    postid = table.Column<string>(nullable: false),
                    tagid = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_posttagbridges", x => new { x.postid, x.tagid });
                    table.ForeignKey(
                        name: "fk_posttagbridges_posts_postid",
                        column: x => x.postid,
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_posttagbridges_tags_tagid",
                        column: x => x.tagid,
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_categories_parentid",
                table: "categories",
                column: "parentid");

            migrationBuilder.CreateIndex(
                name: "ix_posts_authorid",
                table: "posts",
                column: "authorid");

            migrationBuilder.CreateIndex(
                name: "ix_posts_categoryid",
                table: "posts",
                column: "categoryid");

            migrationBuilder.CreateIndex(
                name: "ix_posts_posterid",
                table: "posts",
                column: "posterid");

            migrationBuilder.CreateIndex(
                name: "ix_posttagbridges_tagid",
                table: "posttagbridges",
                column: "tagid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "posttagbridges");

            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
