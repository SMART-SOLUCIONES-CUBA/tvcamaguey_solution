namespace SmartSolucionesCuba.TVCamaguey.Cloud.Syncronization.Handling.Common.Models
{
    public class ProductionTeamSyncModel : SAPRESSC.Syncronization.Handling.Common.Models.ISyncModel<Persistence.Entities.ProductionTeam>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public System.Func<Persistence.Entities.ProductionTeam, bool> Finder => e => e.Id == Id;

        public Persistence.Entities.ProductionTeam Export()
        {
            var entity = new Persistence.Entities.ProductionTeam
            {
                Id = Id
            };

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.ProductionTeam entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }

        public void Mergue(Persistence.Entities.ProductionTeam entity)
        {
            entity.Name = Name;
            entity.Description = Description;
        }
    }
}
