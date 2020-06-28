namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.EF.Repositories
{
    public class TagRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.Tag, string>
    {
        public TagRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
