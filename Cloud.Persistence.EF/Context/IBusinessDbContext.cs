using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.EF.Context
{
    public interface IBusinessDbContext :
        SAPRESSC.Middleware.Identity.Persistence.EF.Context.IIdentityMiddlewarePersistenceDbContext
    {
        DbSet<Entities.Award> Awards { get; set; }
        DbSet<Entities.Blog> Blogs { get; set; }
        DbSet<Entities.Employ> Employs { get; set; }
        DbSet<Entities.EmployAdministrationRoleBridge> EmployAdministrationRoleBridges { get; set; }
        DbSet<Entities.EmployAwardBridge> EmployAwardBridges { get; set; }
        DbSet<Entities.EmployBlogBridge> EmployBlogBridges { get; set; }
        DbSet<Entities.Presentation> Presentations { get; set; }
        DbSet<Entities.ProductionTeam> ProductionTeams { get; set; }
        DbSet<Entities.ProductionTeamEmployBridge> ProductionTeamEmployBridges { get; set; }
        DbSet<Entities.Show> Shows { get; set; }
    }
}
