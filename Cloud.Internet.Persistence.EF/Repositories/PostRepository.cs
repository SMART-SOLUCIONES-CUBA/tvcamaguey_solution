using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.Entities;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.EF.Repositories
{
    public class PostRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.Post, string>
    {
        public PostRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override Func<Post, object> GetPagingOrderSelector()
        {
            return e => e.PostedAt;
        }

        public override bool IsPagingOrderDescending()
        {
            return true;
        }

        protected override IQueryable<Post> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(e => e.Category)
                .Include(e => e.TagBridges).ThenInclude(b => b.Tag)
                .Include(e => e.Poster)
                .Include(e => e.Author).ThenInclude(e => e.Identity).ThenInclude(e => e.Image);
        }
    }
}
