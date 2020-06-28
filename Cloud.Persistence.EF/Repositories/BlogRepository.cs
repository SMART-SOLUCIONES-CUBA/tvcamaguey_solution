using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.EF.Repositories
{
    public class BlogRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.Blog, System.Guid>
    {
        public BlogRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Entities.Blog> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(e => e.EmployBridges).ThenInclude(b => b.Employ);
        }
    }
}