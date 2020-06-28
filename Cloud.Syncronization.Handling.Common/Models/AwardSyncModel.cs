namespace SmartSolucionesCuba.TVCamaguey.Cloud.Syncronization.Handling.Common.Models
{
    public class AwardSyncModel : SAPRESSC.Syncronization.Handling.Common.Models.ISyncModel<Persistence.Entities.Award>
    {
        public System.Guid Id { get; set; }

        public string Title { get; set; }

        public string Edition { get; set; }

        public bool IsInstitutional { get; set; }

        public string Description { get; set; }

        public System.Func<Persistence.Entities.Award, bool> Finder => e => e.Id == Id;

        public Persistence.Entities.Award Export()
        {
            var entity = new Persistence.Entities.Award
            {
                Id = Id
            };

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.Award entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Edition = entity.Edition;
            IsInstitutional = entity.IsInstitutional;
            Description = entity.Description;
        }

        public void Mergue(Persistence.Entities.Award entity)
        {
            entity.Title = Title;
            entity.Edition = Edition;
            entity.IsInstitutional = IsInstitutional;
            entity.Description = Description;
        }
    }
}
