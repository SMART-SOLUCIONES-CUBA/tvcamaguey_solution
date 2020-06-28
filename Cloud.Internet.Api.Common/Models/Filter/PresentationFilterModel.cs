namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models.Filter
{
    public class PresentationFilterModel : SAPRESSC.Core.Persistence.Models.Filter.IEntityFilterModel<Cloud.Persistence.Entities.Presentation, System.Guid>
    {
        public System.DateTime StartAt { get; set; }

        public System.DateTime EndAt { get; set; }

        public System.Linq.Expressions.Expression<System.Func<Cloud.Persistence.Entities.Presentation, bool>> GetSelector()
        {
            if (EndAt == default(System.DateTime))
            {
                return e => e.At >= StartAt;
            }

            return e => e.At >= StartAt && e.At < EndAt;
        }
    }
}
