namespace SmartSolucionesCuba.TVCamaguey.Cloud.Syncronization.Handling.Common.Models
{
    public class PresentationSyncModel : SAPRESSC.Syncronization.Handling.Common.Models.ISyncModel<Persistence.Entities.Presentation>
    {
        public System.Guid Id { get; set; }

        public System.DateTime At { get; set; }

        public System.Guid ShowId { get; set; }

        public System.Guid ProductionTeamId { get; set; }

        public string About { get; set; }

        public System.Func<Persistence.Entities.Presentation, bool> Finder => e => e.Id == Id;

        public Persistence.Entities.Presentation Export()
        {
            var entity = new Persistence.Entities.Presentation
            {
                Id = Id
            };

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.Presentation entity)
        {
            Id = entity.Id;
            At = entity.At;
            ShowId = entity.ShowId;
            ProductionTeamId = entity.ProductionTeamId;
            About = entity.About;
        }

        public void Mergue(Persistence.Entities.Presentation entity)
        {
            entity.At = At;
            entity.ShowId = ShowId;
            entity.ProductionTeamId = ProductionTeamId;
            entity.About = About;
        }
    }
}
