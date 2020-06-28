namespace SmartSolucionesCuba.TVCamaguey.Cloud.Syncronization.Handling.Common.Models
{
    public class ShowSyncModel : SAPRESSC.Syncronization.Handling.Common.Models.ISyncModel<Persistence.Entities.Show>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public System.Guid PosterId { get; set; }

        public System.Guid LogoId { get; set; }

        public string About { get; set; }

        public System.Func<Persistence.Entities.Show, bool> Finder => e => e.Id == Id;

        public Persistence.Entities.Show Export()
        {
            var entity = new Persistence.Entities.Show
            {
                Id = Id
            };

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.Show entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            PosterId = entity.PosterId;
            LogoId = entity.LogoId;
            About = entity.About;
        }

        public void Mergue(Persistence.Entities.Show entity)
        {
            entity.Name = Name;
            entity.PosterId = PosterId;
            entity.LogoId = LogoId;
            entity.About = About;
        }
    }
}
