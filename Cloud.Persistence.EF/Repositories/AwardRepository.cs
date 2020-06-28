using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.EF.Repositories
{
    public class AwardRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.Award, System.Guid>
    {
        public AwardRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.Award> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(e => e.EmployBridges).ThenInclude(b => b.Employ);
        }
    }
}
