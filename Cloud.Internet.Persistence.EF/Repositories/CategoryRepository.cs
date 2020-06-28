namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.EF.Repositories
{
    public class CategoryRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.Category, string>
    {
        public CategoryRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
