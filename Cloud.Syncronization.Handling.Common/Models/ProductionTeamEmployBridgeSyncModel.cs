namespace SmartSolucionesCuba.TVCamaguey.Cloud.Syncronization.Handling.Common.Models
{
    public class ProductionTeamEmployBridgeSyncModel : SAPRESSC.Syncronization.Handling.Common.Models.ISyncModel<Persistence.Entities.ProductionTeamEmployBridge>
    {
        public System.Guid ProductionTeamId { get; set; }

        public System.Guid EmployId { get; set; }

        public Persistence.Enumerations.ProductionRole ProductionRole { get; set; }

        public System.Func<Persistence.Entities.ProductionTeamEmployBridge, bool> Finder => e => e.EmployId == EmployId && e.ProductionTeamId == ProductionTeamId;

        public Persistence.Entities.ProductionTeamEmployBridge Export()
        {
            var entity = new Persistence.Entities.ProductionTeamEmployBridge
            {
                ProductionTeamId = ProductionTeamId,
                EmployId = EmployId
            };

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.ProductionTeamEmployBridge entity)
        {
            ProductionTeamId = entity.ProductionTeamId;
            EmployId = entity.EmployId;
            ProductionRole = entity.ProductionRole;
        }

        public void Mergue(Persistence.Entities.ProductionTeamEmployBridge entity)
        {
            entity.ProductionRole = ProductionRole;
        }
    }
}
