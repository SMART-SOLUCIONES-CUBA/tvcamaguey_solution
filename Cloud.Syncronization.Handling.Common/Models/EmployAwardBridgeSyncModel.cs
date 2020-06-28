namespace SmartSolucionesCuba.TVCamaguey.Cloud.Syncronization.Handling.Common.Models
{
    public class EmployAwardBridgeSyncModel : SAPRESSC.Syncronization.Handling.Common.Models.ISyncModel<Persistence.Entities.EmployAwardBridge>
    {
        public System.Guid EmployId { get; set; }

        public System.Guid AwardId { get; set; }

        public System.Func<Persistence.Entities.EmployAwardBridge, bool> Finder => e => e.EmployId == EmployId && e.AwardId == AwardId;

        public Persistence.Entities.EmployAwardBridge Export()
        {
            var entity = new Persistence.Entities.EmployAwardBridge
            {
                EmployId = EmployId,
                AwardId = AwardId
            };

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.EmployAwardBridge entity)
        {
            EmployId = entity.EmployId;
            AwardId = entity.AwardId;
        }

        public void Mergue(Persistence.Entities.EmployAwardBridge entity)
        {
        }
    }
}
