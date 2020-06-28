namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models.Filter
{
    public class PostFilterModel : SAPRESSC.Core.Persistence.Models.Filter.IEntityFilterModel<Persistence.Entities.Post, string>
    {
        public System.DateTime PostedSameOrAfterAt { get; set; }

        public System.Linq.Expressions.Expression<System.Func<Persistence.Entities.Post, bool>> GetSelector()
        {
            if (PostedSameOrAfterAt == default(System.DateTime))
            {
                return e => true;
            }

            return e => e.PostedAt >= PostedSameOrAfterAt;
        }
    }
}
