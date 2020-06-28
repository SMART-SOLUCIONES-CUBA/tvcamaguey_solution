using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Providers.Facade;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Syncronization.Persistence.EF.Helpers.Facade;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.EF.Context;
using SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.Entities;
using System.Collections.Generic;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Persistence.EF.Context
{
    public class ApplicationDbContext : SAPRESSC.Syncronization.Persistence.EF.Context.AbstractTraceableSycronizableDbContext, IInternetApplicationBaseDbContext
    {
        public ApplicationDbContext(IEnumerable<ISyncronizationDbChangeHandleHelperFacade> syncronizationDbChangeHandleHelpers, IEnumerable<IDbSchemaConfigurationProviderFacade> dbSchemaConfigurationProviders, DbContextOptions options) : base(syncronizationDbChangeHandleHelpers, dbSchemaConfigurationProviders, options)
        {
        }

        public override bool TraceChanges => false;

        public DbSet<Internet.Persistence.Entities.Post> Posts { get; set; }
        public DbSet<Internet.Persistence.Entities.Category> Categories { get; set; }
        public DbSet<Internet.Persistence.Entities.Tag> Tags { get; set; }
        public DbSet<Internet.Persistence.Entities.PostTagBridge> PostTagBridges { get; set; }
        public DbSet<Internet.Persistence.Entities.InstitutionInfoUpdateRecord> InstitutionInfoUpdateRecords { get; set; }
        public DbSet<Internet.Persistence.Entities.InstitutionBusinessUpdateRecord> InstitutionBusinessUpdateRecords { get; set; }
        public DbSet<Internet.Persistence.Entities.InstitutionInfoNomenclator> InstitutionInfoNomenclators { get; set; }

        public DbSet<Award> Awards { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Employ> Employs { get; set; }
        public DbSet<EmployAdministrationRoleBridge> EmployAdministrationRoleBridges { get; set; }
        public DbSet<EmployAwardBridge> EmployAwardBridges { get; set; }
        public DbSet<EmployBlogBridge> EmployBlogBridges { get; set; }
        public DbSet<Presentation> Presentations { get; set; }
        public DbSet<ProductionTeam> ProductionTeams { get; set; }
        public DbSet<ProductionTeamEmployBridge> ProductionTeamEmployBridges { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<PersonIdentity> PersonIdentities { get; set; }
        public DbSet<OrganizationIdentity> OrganizationIdentities { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<EmailContactItem> EmailContactItems { get; set; }
        public DbSet<PhoneContactItem> PhoneContactItems { get; set; }
        public DbSet<SocialContactItem> SocialContactItems { get; set; }
        public DbSet<WebSiteContactItem> WebSiteContactItems { get; set; }
        public DbSet<PostalContactItem> PostalContactItems { get; set; }
        public DbSet<PersonEmailContactItemBridge> PersonEmailContactItemBridges { get; set; }
        public DbSet<PersonPhoneContactItemBridge> PersonPhoneContactItemBridges { get; set; }
        public DbSet<PersonPostalContactItemBridge> PersonPostalContactItemBridges { get; set; }
        public DbSet<PersonSocialContactItemBridge> PersonSocialContactItemBridges { get; set; }
        public DbSet<PersonWebSiteContactItemBridge> PersonWebSiteContactItemBridges { get; set; }
        public DbSet<OrganizationEmailContactItemBridge> OrganizationEmailContactItemBridges { get; set; }
        public DbSet<OrganizationPhoneContactItemBridge> OrganizationPhoneContactItemBridges { get; set; }
        public DbSet<OrganizationPostalContactItemBridge> OrganizationPostalContactItemBridges { get; set; }
        public DbSet<OrganizationSocialContactItemBridge> OrganizationSocialContactItemBridges { get; set; }
        public DbSet<OrganizationWebSiteContactItemBridge> OrganizationWebSiteContactItemBridges { get; set; }
        public DbSet<Thumbnail> Thumbnails { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Locality> Localities { get; set; }
        public DbSet<CountryRegion> CountryRegions { get; set; }
        public DbSet<StateRegion> StateRegions { get; set; }
        public DbSet<MunicipalityRegion> MunicipalityRegions { get; set; }
        public DbSet<LocalityRegion> LocalityRegions { get; set; }
    }
}
