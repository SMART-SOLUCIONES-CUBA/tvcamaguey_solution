using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Migrations
{
    public partial class InitialSchemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tvcamagueycloudinternet_sapressc_core_securitylocal");

            migrationBuilder.EnsureSchema(
                name: "tvcamagueycloudinternet_sapressc_middleware_identity");

            migrationBuilder.EnsureSchema(
                name: "tvcamagueycloudinternet_sapressc_middleware_media");

            migrationBuilder.EnsureSchema(
                name: "tvcamagueycloudinternet_sapressc_middleware_region");

            migrationBuilder.CreateTable(
                name: "aspnetroles",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(maxLength: 256, nullable: true),
                    normalizedname = table.Column<string>(maxLength: 256, nullable: true),
                    concurrencystamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnetroles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "awards",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    title = table.Column<string>(nullable: false),
                    edition = table.Column<string>(nullable: false),
                    isinstitutional = table.Column<bool>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_awards", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "blogs",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    title = table.Column<string>(nullable: false),
                    url = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_blogs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "institutionbusinessupdaterecords",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_institutionbusinessupdaterecords", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "institutioninfonomenclators",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_institutioninfonomenclators", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "institutioninfoupdaterecords",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_institutioninfoupdaterecords", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "productionteams",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_productionteams", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "traces",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    type = table.Column<int>(nullable: false),
                    author = table.Column<string>(nullable: true),
                    registered = table.Column<DateTime>(nullable: false),
                    entityname = table.Column<string>(nullable: false),
                    beforedata = table.Column<string>(nullable: false),
                    afterdata = table.Column<string>(nullable: true),
                    nextid = table.Column<long>(nullable: false),
                    signature = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_traces", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "backendgroups",
                schema: "tvcamagueycloudinternet_sapressc_core_securitylocal",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_backendgroups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "backendroles",
                schema: "tvcamagueycloudinternet_sapressc_core_securitylocal",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_backendroles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "backendusers",
                schema: "tvcamagueycloudinternet_sapressc_core_securitylocal",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    enabled = table.Column<bool>(nullable: false),
                    password = table.Column<string>(nullable: false),
                    passwordchangerequired = table.Column<bool>(nullable: false),
                    hasfullaccess = table.Column<bool>(nullable: false),
                    created = table.Column<DateTime>(nullable: false),
                    lastsessiondate = table.Column<DateTime>(nullable: false),
                    failauthenticationcount = table.Column<int>(nullable: false),
                    signature = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_backendusers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "emailcontactitems",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    label = table.Column<string>(nullable: true),
                    emailaddress = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_emailcontactitems", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "phonecontactitems",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    label = table.Column<string>(nullable: true),
                    phonenumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_phonecontactitems", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "socialcontactitems",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    label = table.Column<string>(nullable: true),
                    socialid = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_socialcontactitems", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "websitecontactitems",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    label = table.Column<string>(nullable: true),
                    url = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_websitecontactitems", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "thumbnails",
                schema: "tvcamagueycloudinternet_sapressc_middleware_media",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    contenttype = table.Column<string>(nullable: false),
                    length = table.Column<long>(nullable: false),
                    about = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_thumbnails", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                schema: "tvcamagueycloudinternet_sapressc_middleware_region",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    isocode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_countries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "aspnetroleclaims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    roleid = table.Column<string>(nullable: false),
                    claimtype = table.Column<string>(nullable: true),
                    claimvalue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnetroleclaims", x => x.id);
                    table.ForeignKey(
                        name: "fk_aspnetroleclaims_aspnetroles_roleid",
                        column: x => x.roleid,
                        principalTable: "aspnetroles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "backendgroupbackendrolebridges",
                schema: "tvcamagueycloudinternet_sapressc_core_securitylocal",
                columns: table => new
                {
                    groupid = table.Column<Guid>(nullable: false),
                    roleid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_backendgroupbackendrolebridges", x => new { x.groupid, x.roleid });
                    table.ForeignKey(
                        name: "fk_backendgroupbackendrolebridges_backendgroups_groupid",
                        column: x => x.groupid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_core_securitylocal",
                        principalTable: "backendgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_backendgroupbackendrolebridges_backendroles_roleid",
                        column: x => x.roleid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_core_securitylocal",
                        principalTable: "backendroles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "backenduserbackendgroupbridges",
                schema: "tvcamagueycloudinternet_sapressc_core_securitylocal",
                columns: table => new
                {
                    userid = table.Column<string>(nullable: false),
                    groupid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_backenduserbackendgroupbridges", x => new { x.userid, x.groupid });
                    table.ForeignKey(
                        name: "fk_backenduserbackendgroupbridges_backendgroups_groupid",
                        column: x => x.groupid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_core_securitylocal",
                        principalTable: "backendgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_backenduserbackendgroupbridges_backendusers_userid",
                        column: x => x.userid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_core_securitylocal",
                        principalTable: "backendusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "images",
                schema: "tvcamagueycloudinternet_sapressc_middleware_media",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    createat = table.Column<DateTime>(nullable: false),
                    contenttype = table.Column<string>(nullable: false),
                    length = table.Column<long>(nullable: false),
                    about = table.Column<string>(maxLength: 255, nullable: true),
                    height = table.Column<int>(nullable: false),
                    width = table.Column<int>(nullable: false),
                    thumbnailid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_images", x => x.id);
                    table.ForeignKey(
                        name: "fk_images_thumbnails_thumbnailid",
                        column: x => x.thumbnailid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_media",
                        principalTable: "thumbnails",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "videos",
                schema: "tvcamagueycloudinternet_sapressc_middleware_media",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    createat = table.Column<DateTime>(nullable: false),
                    extension = table.Column<string>(nullable: false),
                    length = table.Column<long>(nullable: false),
                    about = table.Column<string>(maxLength: 255, nullable: true),
                    duration = table.Column<TimeSpan>(nullable: false),
                    format = table.Column<string>(nullable: false),
                    framesize = table.Column<string>(nullable: false),
                    colormodel = table.Column<string>(nullable: false),
                    fps = table.Column<double>(nullable: false),
                    audioformat = table.Column<string>(nullable: false),
                    audiochanneloutput = table.Column<string>(nullable: false),
                    thumbnailid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_videos", x => x.id);
                    table.ForeignKey(
                        name: "fk_videos_thumbnails_thumbnailid",
                        column: x => x.thumbnailid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_media",
                        principalTable: "thumbnails",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "states",
                schema: "tvcamagueycloudinternet_sapressc_middleware_region",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    countryid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_states", x => x.id);
                    table.ForeignKey(
                        name: "fk_states_countries_countryid",
                        column: x => x.countryid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_region",
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shows",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    posterid = table.Column<Guid>(nullable: false),
                    logoid = table.Column<Guid>(nullable: false),
                    about = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shows", x => x.id);
                    table.ForeignKey(
                        name: "fk_shows_images_logoid",
                        column: x => x.logoid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_shows_images_posterid",
                        column: x => x.posterid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationidentities",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    borndate = table.Column<DateTime>(nullable: false),
                    about = table.Column<string>(nullable: true),
                    ownerid = table.Column<Guid>(nullable: true),
                    imageid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationidentities", x => x.id);
                    table.ForeignKey(
                        name: "fk_organizationidentities_images_imageid",
                        column: x => x.imageid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_organizationidentities_organizationidentities_ownerid",
                        column: x => x.ownerid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "personidentities",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    borndate = table.Column<DateTime>(nullable: false),
                    about = table.Column<string>(nullable: true),
                    lastname = table.Column<string>(nullable: true),
                    alias = table.Column<string>(nullable: true),
                    sex = table.Column<int>(nullable: false),
                    civilstatus = table.Column<int>(nullable: false),
                    imageid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personidentities", x => x.id);
                    table.ForeignKey(
                        name: "fk_personidentities_images_imageid",
                        column: x => x.imageid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_media",
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "municipalities",
                schema: "tvcamagueycloudinternet_sapressc_middleware_region",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    stateid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_municipalities", x => x.id);
                    table.ForeignKey(
                        name: "fk_municipalities_states_stateid",
                        column: x => x.stateid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_region",
                        principalTable: "states",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "presentations",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    at = table.Column<DateTime>(nullable: false),
                    showid = table.Column<Guid>(nullable: false),
                    productionteamid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_presentations", x => x.id);
                    table.ForeignKey(
                        name: "fk_presentations_productionteams_productionteamid",
                        column: x => x.productionteamid,
                        principalTable: "productionteams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_presentations_shows_showid",
                        column: x => x.showid,
                        principalTable: "shows",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationemailcontactitembridges",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                columns: table => new
                {
                    organizationid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationemailcontactitembridges", x => new { x.organizationid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_organizationemailcontactitembridges_emailcontactitems_contac~",
                        column: x => x.contactitemid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                        principalTable: "emailcontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organizationemailcontactitembridges_organizationidentities_o~",
                        column: x => x.organizationid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationphonecontactitembridges",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                columns: table => new
                {
                    organizationid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationphonecontactitembridges", x => new { x.organizationid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_organizationphonecontactitembridges_phonecontactitems_contac~",
                        column: x => x.contactitemid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                        principalTable: "phonecontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organizationphonecontactitembridges_organizationidentities_o~",
                        column: x => x.organizationid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationsocialcontactitembridges",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                columns: table => new
                {
                    organizationid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationsocialcontactitembridges", x => new { x.organizationid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_organizationsocialcontactitembridges_socialcontactitems_cont~",
                        column: x => x.contactitemid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                        principalTable: "socialcontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organizationsocialcontactitembridges_organizationidentities_~",
                        column: x => x.organizationid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationwebsitecontactitembridges",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                columns: table => new
                {
                    organizationid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationwebsitecontactitembridges", x => new { x.organizationid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_organizationwebsitecontactitembridges_websitecontactitems_co~",
                        column: x => x.contactitemid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                        principalTable: "websitecontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organizationwebsitecontactitembridges_organizationidentities~",
                        column: x => x.organizationid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnetusers",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    username = table.Column<string>(maxLength: 256, nullable: true),
                    normalizedusername = table.Column<string>(maxLength: 256, nullable: true),
                    email = table.Column<string>(maxLength: 256, nullable: true),
                    normalizedemail = table.Column<string>(maxLength: 256, nullable: true),
                    emailconfirmed = table.Column<bool>(nullable: false),
                    passwordhash = table.Column<string>(nullable: true),
                    securitystamp = table.Column<string>(nullable: true),
                    concurrencystamp = table.Column<string>(nullable: true),
                    phonenumber = table.Column<string>(nullable: true),
                    phonenumberconfirmed = table.Column<bool>(nullable: false),
                    twofactorenabled = table.Column<bool>(nullable: false),
                    lockoutend = table.Column<DateTimeOffset>(nullable: true),
                    lockoutenabled = table.Column<bool>(nullable: false),
                    accessfailedcount = table.Column<int>(nullable: false),
                    identityid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnetusers", x => x.id);
                    table.ForeignKey(
                        name: "fk_aspnetusers_personidentities_identityid",
                        column: x => x.identityid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employs",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    identityid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employs", x => x.id);
                    table.ForeignKey(
                        name: "fk_employs_personidentities_identityid",
                        column: x => x.identityid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "memberships",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                columns: table => new
                {
                    personid = table.Column<Guid>(nullable: false),
                    organizationid = table.Column<Guid>(nullable: false),
                    role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_memberships", x => new { x.organizationid, x.personid });
                    table.ForeignKey(
                        name: "fk_memberships_organizationidentities_organizationid",
                        column: x => x.organizationid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_memberships_personidentities_personid",
                        column: x => x.personid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personemailcontactitembridges",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                columns: table => new
                {
                    personid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personemailcontactitembridges", x => new { x.personid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_personemailcontactitembridges_emailcontactitems_contactitemid",
                        column: x => x.contactitemid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                        principalTable: "emailcontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_personemailcontactitembridges_personidentities_personid",
                        column: x => x.personid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personphonecontactitembridges",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                columns: table => new
                {
                    personid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personphonecontactitembridges", x => new { x.personid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_personphonecontactitembridges_phonecontactitems_contactitemid",
                        column: x => x.contactitemid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                        principalTable: "phonecontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_personphonecontactitembridges_personidentities_personid",
                        column: x => x.personid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personsocialcontactitembridges",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                columns: table => new
                {
                    personid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personsocialcontactitembridges", x => new { x.personid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_personsocialcontactitembridges_socialcontactitems_contactite~",
                        column: x => x.contactitemid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                        principalTable: "socialcontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_personsocialcontactitembridges_personidentities_personid",
                        column: x => x.personid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personwebsitecontactitembridges",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                columns: table => new
                {
                    personid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personwebsitecontactitembridges", x => new { x.personid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_personwebsitecontactitembridges_websitecontactitems_contacti~",
                        column: x => x.contactitemid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                        principalTable: "websitecontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_personwebsitecontactitembridges_personidentities_personid",
                        column: x => x.personid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "localities",
                schema: "tvcamagueycloudinternet_sapressc_middleware_region",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    municipalityid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_localities", x => x.id);
                    table.ForeignKey(
                        name: "fk_localities_municipalities_municipalityid",
                        column: x => x.municipalityid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_region",
                        principalTable: "municipalities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnetuserclaims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userid = table.Column<string>(nullable: false),
                    claimtype = table.Column<string>(nullable: true),
                    claimvalue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnetuserclaims", x => x.id);
                    table.ForeignKey(
                        name: "fk_aspnetuserclaims_aspnetusers_userid",
                        column: x => x.userid,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnetuserlogins",
                columns: table => new
                {
                    loginprovider = table.Column<string>(nullable: false),
                    providerkey = table.Column<string>(nullable: false),
                    providerdisplayname = table.Column<string>(nullable: true),
                    userid = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnetuserlogins", x => new { x.loginprovider, x.providerkey });
                    table.ForeignKey(
                        name: "fk_aspnetuserlogins_aspnetusers_userid",
                        column: x => x.userid,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnetuserroles",
                columns: table => new
                {
                    userid = table.Column<string>(nullable: false),
                    roleid = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnetuserroles", x => new { x.userid, x.roleid });
                    table.ForeignKey(
                        name: "fk_aspnetuserroles_aspnetroles_roleid",
                        column: x => x.roleid,
                        principalTable: "aspnetroles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_aspnetuserroles_aspnetusers_userid",
                        column: x => x.userid,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnetusertokens",
                columns: table => new
                {
                    userid = table.Column<string>(nullable: false),
                    loginprovider = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnetusertokens", x => new { x.userid, x.loginprovider, x.name });
                    table.ForeignKey(
                        name: "fk_aspnetusertokens_aspnetusers_userid",
                        column: x => x.userid,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employadministrationrolebridges",
                columns: table => new
                {
                    employid = table.Column<Guid>(nullable: false),
                    administrativerole = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employadministrationrolebridges", x => new { x.employid, x.administrativerole });
                    table.ForeignKey(
                        name: "fk_employadministrationrolebridges_employs_employid",
                        column: x => x.employid,
                        principalTable: "employs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employawardbridges",
                columns: table => new
                {
                    employid = table.Column<Guid>(nullable: false),
                    awardid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employawardbridges", x => new { x.employid, x.awardid });
                    table.ForeignKey(
                        name: "fk_employawardbridges_awards_awardid",
                        column: x => x.awardid,
                        principalTable: "awards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_employawardbridges_employs_employid",
                        column: x => x.employid,
                        principalTable: "employs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employblogbridges",
                columns: table => new
                {
                    employid = table.Column<Guid>(nullable: false),
                    blogid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employblogbridges", x => new { x.employid, x.blogid });
                    table.ForeignKey(
                        name: "fk_employblogbridges_blogs_blogid",
                        column: x => x.blogid,
                        principalTable: "blogs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_employblogbridges_employs_employid",
                        column: x => x.employid,
                        principalTable: "employs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productionteamemploybridges",
                columns: table => new
                {
                    productionteamid = table.Column<Guid>(nullable: false),
                    employid = table.Column<Guid>(nullable: false),
                    productionrole = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_productionteamemploybridges", x => new { x.employid, x.productionteamid, x.productionrole });
                    table.ForeignKey(
                        name: "fk_productionteamemploybridges_employs_employid",
                        column: x => x.employid,
                        principalTable: "employs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_productionteamemploybridges_productionteams_productionteamid",
                        column: x => x.productionteamid,
                        principalTable: "productionteams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "abstractregion",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    discriminator = table.Column<string>(nullable: false),
                    countryid = table.Column<Guid>(nullable: true),
                    localityid = table.Column<Guid>(nullable: true),
                    municipalityid = table.Column<Guid>(nullable: true),
                    stateid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_abstractregion", x => x.id);
                    table.ForeignKey(
                        name: "fk_abstractregion_countries_countryid",
                        column: x => x.countryid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_region",
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_abstractregion_localities_localityid",
                        column: x => x.localityid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_region",
                        principalTable: "localities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_abstractregion_municipalities_countryid",
                        column: x => x.countryid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_region",
                        principalTable: "municipalities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_abstractregion_states_stateid",
                        column: x => x.stateid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_region",
                        principalTable: "states",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "postalcontactitems",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    label = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: false),
                    regionid = table.Column<Guid>(nullable: false),
                    zipcode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_postalcontactitems", x => x.id);
                    table.ForeignKey(
                        name: "fk_postalcontactitems_abstractregion_regionid",
                        column: x => x.regionid,
                        principalTable: "abstractregion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationpostalcontactitembridges",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                columns: table => new
                {
                    organizationid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationpostalcontactitembridges", x => new { x.organizationid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_organizationpostalcontactitembridges_postalcontactitems_cont~",
                        column: x => x.contactitemid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                        principalTable: "postalcontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organizationpostalcontactitembridges_organizationidentities_~",
                        column: x => x.organizationid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personpostalcontactitembridges",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                columns: table => new
                {
                    personid = table.Column<Guid>(nullable: false),
                    contactitemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personpostalcontactitembridges", x => new { x.personid, x.contactitemid });
                    table.ForeignKey(
                        name: "fk_personpostalcontactitembridges_postalcontactitems_contactite~",
                        column: x => x.contactitemid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                        principalTable: "postalcontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_personpostalcontactitembridges_personidentities_personid",
                        column: x => x.personid,
                        principalSchema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_abstractregion_countryid",
                table: "abstractregion",
                column: "countryid");

            migrationBuilder.CreateIndex(
                name: "ix_abstractregion_localityid",
                table: "abstractregion",
                column: "localityid");

            migrationBuilder.CreateIndex(
                name: "ix_abstractregion_stateid",
                table: "abstractregion",
                column: "stateid");

            migrationBuilder.CreateIndex(
                name: "ix_aspnetroleclaims_roleid",
                table: "aspnetroleclaims",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "rolenameindex",
                table: "aspnetroles",
                column: "normalizedname",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_aspnetuserclaims_userid",
                table: "aspnetuserclaims",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_aspnetuserlogins_userid",
                table: "aspnetuserlogins",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "ix_aspnetuserroles_roleid",
                table: "aspnetuserroles",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "ix_aspnetusers_identityid",
                table: "aspnetusers",
                column: "identityid");

            migrationBuilder.CreateIndex(
                name: "emailindex",
                table: "aspnetusers",
                column: "normalizedemail");

            migrationBuilder.CreateIndex(
                name: "usernameindex",
                table: "aspnetusers",
                column: "normalizedusername",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_employawardbridges_awardid",
                table: "employawardbridges",
                column: "awardid");

            migrationBuilder.CreateIndex(
                name: "ix_employblogbridges_blogid",
                table: "employblogbridges",
                column: "blogid");

            migrationBuilder.CreateIndex(
                name: "ix_employs_identityid",
                table: "employs",
                column: "identityid");

            migrationBuilder.CreateIndex(
                name: "ix_presentations_productionteamid",
                table: "presentations",
                column: "productionteamid");

            migrationBuilder.CreateIndex(
                name: "ix_presentations_showid",
                table: "presentations",
                column: "showid");

            migrationBuilder.CreateIndex(
                name: "ix_productionteamemploybridges_productionteamid",
                table: "productionteamemploybridges",
                column: "productionteamid");

            migrationBuilder.CreateIndex(
                name: "ix_shows_logoid",
                table: "shows",
                column: "logoid");

            migrationBuilder.CreateIndex(
                name: "ix_shows_posterid",
                table: "shows",
                column: "posterid");

            migrationBuilder.CreateIndex(
                name: "ix_backendgroupbackendrolebridges_roleid",
                schema: "tvcamagueycloudinternet_sapressc_core_securitylocal",
                table: "backendgroupbackendrolebridges",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "ix_backenduserbackendgroupbridges_groupid",
                schema: "tvcamagueycloudinternet_sapressc_core_securitylocal",
                table: "backenduserbackendgroupbridges",
                column: "groupid");

            migrationBuilder.CreateIndex(
                name: "ix_memberships_personid",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                table: "memberships",
                column: "personid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationemailcontactitembridges_contactitemid",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                table: "organizationemailcontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationidentities_imageid",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                table: "organizationidentities",
                column: "imageid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationidentities_ownerid",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                table: "organizationidentities",
                column: "ownerid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationphonecontactitembridges_contactitemid",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                table: "organizationphonecontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationpostalcontactitembridges_contactitemid",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                table: "organizationpostalcontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationsocialcontactitembridges_contactitemid",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                table: "organizationsocialcontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationwebsitecontactitembridges_contactitemid",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                table: "organizationwebsitecontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_personemailcontactitembridges_contactitemid",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                table: "personemailcontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_personidentities_imageid",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                table: "personidentities",
                column: "imageid");

            migrationBuilder.CreateIndex(
                name: "ix_personphonecontactitembridges_contactitemid",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                table: "personphonecontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_personpostalcontactitembridges_contactitemid",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                table: "personpostalcontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_personsocialcontactitembridges_contactitemid",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                table: "personsocialcontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_personwebsitecontactitembridges_contactitemid",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                table: "personwebsitecontactitembridges",
                column: "contactitemid");

            migrationBuilder.CreateIndex(
                name: "ix_postalcontactitems_regionid",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity",
                table: "postalcontactitems",
                column: "regionid");

            migrationBuilder.CreateIndex(
                name: "ix_images_thumbnailid",
                schema: "tvcamagueycloudinternet_sapressc_middleware_media",
                table: "images",
                column: "thumbnailid");

            migrationBuilder.CreateIndex(
                name: "ix_videos_thumbnailid",
                schema: "tvcamagueycloudinternet_sapressc_middleware_media",
                table: "videos",
                column: "thumbnailid");

            migrationBuilder.CreateIndex(
                name: "ix_countries_isocode",
                schema: "tvcamagueycloudinternet_sapressc_middleware_region",
                table: "countries",
                column: "isocode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_countries_name",
                schema: "tvcamagueycloudinternet_sapressc_middleware_region",
                table: "countries",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_localities_municipalityid",
                schema: "tvcamagueycloudinternet_sapressc_middleware_region",
                table: "localities",
                column: "municipalityid");

            migrationBuilder.CreateIndex(
                name: "ix_localities_name_municipalityid",
                schema: "tvcamagueycloudinternet_sapressc_middleware_region",
                table: "localities",
                columns: new[] { "name", "municipalityid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_municipalities_stateid",
                schema: "tvcamagueycloudinternet_sapressc_middleware_region",
                table: "municipalities",
                column: "stateid");

            migrationBuilder.CreateIndex(
                name: "ix_municipalities_name_stateid",
                schema: "tvcamagueycloudinternet_sapressc_middleware_region",
                table: "municipalities",
                columns: new[] { "name", "stateid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_states_countryid",
                schema: "tvcamagueycloudinternet_sapressc_middleware_region",
                table: "states",
                column: "countryid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aspnetroleclaims");

            migrationBuilder.DropTable(
                name: "aspnetuserclaims");

            migrationBuilder.DropTable(
                name: "aspnetuserlogins");

            migrationBuilder.DropTable(
                name: "aspnetuserroles");

            migrationBuilder.DropTable(
                name: "aspnetusertokens");

            migrationBuilder.DropTable(
                name: "employadministrationrolebridges");

            migrationBuilder.DropTable(
                name: "employawardbridges");

            migrationBuilder.DropTable(
                name: "employblogbridges");

            migrationBuilder.DropTable(
                name: "institutionbusinessupdaterecords");

            migrationBuilder.DropTable(
                name: "institutioninfonomenclators");

            migrationBuilder.DropTable(
                name: "institutioninfoupdaterecords");

            migrationBuilder.DropTable(
                name: "presentations");

            migrationBuilder.DropTable(
                name: "productionteamemploybridges");

            migrationBuilder.DropTable(
                name: "traces");

            migrationBuilder.DropTable(
                name: "backendgroupbackendrolebridges",
                schema: "tvcamagueycloudinternet_sapressc_core_securitylocal");

            migrationBuilder.DropTable(
                name: "backenduserbackendgroupbridges",
                schema: "tvcamagueycloudinternet_sapressc_core_securitylocal");

            migrationBuilder.DropTable(
                name: "memberships",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "organizationemailcontactitembridges",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "organizationphonecontactitembridges",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "organizationpostalcontactitembridges",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "organizationsocialcontactitembridges",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "organizationwebsitecontactitembridges",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "personemailcontactitembridges",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "personphonecontactitembridges",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "personpostalcontactitembridges",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "personsocialcontactitembridges",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "personwebsitecontactitembridges",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "videos",
                schema: "tvcamagueycloudinternet_sapressc_middleware_media");

            migrationBuilder.DropTable(
                name: "aspnetroles");

            migrationBuilder.DropTable(
                name: "aspnetusers");

            migrationBuilder.DropTable(
                name: "awards");

            migrationBuilder.DropTable(
                name: "blogs");

            migrationBuilder.DropTable(
                name: "shows");

            migrationBuilder.DropTable(
                name: "employs");

            migrationBuilder.DropTable(
                name: "productionteams");

            migrationBuilder.DropTable(
                name: "backendroles",
                schema: "tvcamagueycloudinternet_sapressc_core_securitylocal");

            migrationBuilder.DropTable(
                name: "backendgroups",
                schema: "tvcamagueycloudinternet_sapressc_core_securitylocal");

            migrationBuilder.DropTable(
                name: "backendusers",
                schema: "tvcamagueycloudinternet_sapressc_core_securitylocal");

            migrationBuilder.DropTable(
                name: "organizationidentities",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "emailcontactitems",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "phonecontactitems",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "postalcontactitems",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "socialcontactitems",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "websitecontactitems",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "personidentities",
                schema: "tvcamagueycloudinternet_sapressc_middleware_identity");

            migrationBuilder.DropTable(
                name: "abstractregion");

            migrationBuilder.DropTable(
                name: "images",
                schema: "tvcamagueycloudinternet_sapressc_middleware_media");

            migrationBuilder.DropTable(
                name: "localities",
                schema: "tvcamagueycloudinternet_sapressc_middleware_region");

            migrationBuilder.DropTable(
                name: "thumbnails",
                schema: "tvcamagueycloudinternet_sapressc_middleware_media");

            migrationBuilder.DropTable(
                name: "municipalities",
                schema: "tvcamagueycloudinternet_sapressc_middleware_region");

            migrationBuilder.DropTable(
                name: "states",
                schema: "tvcamagueycloudinternet_sapressc_middleware_region");

            migrationBuilder.DropTable(
                name: "countries",
                schema: "tvcamagueycloudinternet_sapressc_middleware_region");
        }
    }
}
