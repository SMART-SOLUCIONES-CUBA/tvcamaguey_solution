using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.EF.Repositories
{
    public class ShowRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.Show, System.Guid>
    {
        public ShowRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.Show> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(e => e.Logo).ThenInclude(e => e.Thumbnail)
                .Include(e => e.Poster).ThenInclude(e => e.Thumbnail);
        }
    }
}
