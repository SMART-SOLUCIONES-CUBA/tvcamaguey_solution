using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.Entities;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.EF.Repositories
{
    public class EmployRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.Employ, System.Guid>
    {
        public EmployRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Employ> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(e => e.Identity).ThenInclude(e => e.Image)
                .Include(e => e.AdministrationRoleBridges);
        }
    }
}
