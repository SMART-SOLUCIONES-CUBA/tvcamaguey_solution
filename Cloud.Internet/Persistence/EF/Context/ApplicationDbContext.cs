using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Providers.Facade;
using SmartSolucionesCuba.SAPRESSC.Core.Security.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.Entities;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.EF.Context
{
    public class ApplicationDbContext :
        SAPRESSC.Core.Persistence.EF.Context.AbstractTraceableIdentityDbContext<Entities.TerminalUser>,
        IInternetApplicationBaseDbContext
    {
        public ApplicationDbContext(IEnumerable<IDbSchemaConfigurationProviderFacade> dbSchemaConfigurationProviders, DbContextOptions options) : base(dbSchemaConfigurationProviders, options)
        {
        }

        public DbSet<Entities.Post> Posts { get; set; }
        public DbSet<Entities.Category> Categories { get; set; }
        public DbSet<Entities.Tag> Tags { get; set; }
        public DbSet<Entities.PostTagBridge> PostTagBridges { get; set; }
        public DbSet<Entities.InstitutionInfoUpdateRecord> InstitutionInfoUpdateRecords { get; set; }
        public DbSet<Entities.InstitutionBusinessUpdateRecord> InstitutionBusinessUpdateRecords { get; set; }
        public DbSet<Entities.InstitutionInfoNomenclator> InstitutionInfoNomenclators { get; set; }

        public DbSet<Cloud.Persistence.Entities.Award> Awards { get; set; }
        public DbSet<Cloud.Persistence.Entities.Blog> Blogs { get; set; }
        public DbSet<Cloud.Persistence.Entities.Employ> Employs { get; set; }
        public DbSet<Cloud.Persistence.Entities.EmployAdministrationRoleBridge> EmployAdministrationRoleBridges { get; set; }
        public DbSet<Cloud.Persistence.Entities.EmployAwardBridge> EmployAwardBridges { get; set; }
        public DbSet<Cloud.Persistence.Entities.EmployBlogBridge> EmployBlogBridges { get; set; }
        public DbSet<Cloud.Persistence.Entities.Presentation> Presentations { get; set; }
        public DbSet<Cloud.Persistence.Entities.ProductionTeam> ProductionTeams { get; set; }
        public DbSet<Cloud.Persistence.Entities.ProductionTeamEmployBridge> ProductionTeamEmployBridges { get; set; }
        public DbSet<Cloud.Persistence.Entities.Show> Shows { get; set; }

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

        public DbSet<Group> BackendGroups { get; set; }
        public DbSet<UserGroupBridge> BackendUserBackendGroupBridges { get; set; }
        public DbSet<GroupRoleBridge> BackendGroupBackendRoleBridges { get; set; }
        public DbSet<User> BackendUsers { get; set; }
        public DbSet<Role> BackendRoles { get; set; }

        public DbSet<Mailer.Persistence.Entities.UserVerificationMailSendOrder> UserVerificationMailSendOrders { get; set; }
    }
}
