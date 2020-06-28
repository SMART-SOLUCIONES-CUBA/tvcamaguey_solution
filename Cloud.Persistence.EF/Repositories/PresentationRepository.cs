using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.EF.Repositories
{
    public class PresentationRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.Presentation, System.Guid>
    {
        public PresentationRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.Presentation> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(e => e.ProductionTeam).ThenInclude(e => e.EmployBridges).ThenInclude(b => b.Employ).ThenInclude(e => e.Identity).ThenInclude(e => e.Image)
                .Include(e => e.Show).ThenInclude(e => e.Logo)
                .Include(e => e.Show).ThenInclude(e => e.Poster);
        }

        public override System.Func<Entities.Presentation, object> GetPagingOrderSelector()
        {
            return e => e.At;
        }
    }
}
