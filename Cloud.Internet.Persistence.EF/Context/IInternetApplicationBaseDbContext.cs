using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.EF.Context
{
    public interface IInternetApplicationBaseDbContext : Cloud.Persistence.EF.Context.IBusinessDbContext
    {
        DbSet<Entities.Post> Posts { get; set; }
        DbSet<Entities.Category> Categories { get; set; }
        DbSet<Entities.Tag> Tags { get; set; }
        DbSet<Entities.PostTagBridge> PostTagBridges { get; set; }
        DbSet<Entities.InstitutionInfoUpdateRecord> InstitutionInfoUpdateRecords { get; set; }
        DbSet<Entities.InstitutionBusinessUpdateRecord> InstitutionBusinessUpdateRecords { get; set; }
        DbSet<Entities.InstitutionInfoNomenclator> InstitutionInfoNomenclators { get; set; }
    }
}
