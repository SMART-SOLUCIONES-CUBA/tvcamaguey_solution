namespace SmartSolucionesCuba.TVCamaguey.Cloud.Syncronization.Handling.Common.Models
{
    public class BlogSyncModel : SAPRESSC.Syncronization.Handling.Common.Models.ISyncModel<Persistence.Entities.Blog>
    {
        public System.Guid Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public System.Func<Persistence.Entities.Blog, bool> Finder => e => e.Id == Id;

        public Persistence.Entities.Blog Export()
        {
            var entity = new Persistence.Entities.Blog
            {
                Id = Id
            };

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.Blog entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Url = entity.Url;
        }

        public void Mergue(Persistence.Entities.Blog entity)
        {
            entity.Title = Title;
            entity.Url = Url;
        }
    }
}
