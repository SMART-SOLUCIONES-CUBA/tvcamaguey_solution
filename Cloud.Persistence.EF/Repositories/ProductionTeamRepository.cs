using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.EF.Repositories
{
    public class ProductionTeamRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.ProductionTeam, System.Guid>
    {
        public ProductionTeamRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.ProductionTeam> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(e => e.EmployBridges).ThenInclude(b => b.Employ);
        }
    }
}
