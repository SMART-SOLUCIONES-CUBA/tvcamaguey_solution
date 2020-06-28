namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View
{
    public class TagInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Persistence.Entities.Tag, string>
    {
        public string Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.Display(Name = "Name")]
        public string Name { get; set; }

        public Persistence.Entities.Tag Export()
        {
            var slugHelper = new Slugify.SlugHelper();

            var entity = new Persistence.Entities.Tag
            {
                Id = slugHelper.GenerateSlug(Name)
            };

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.Tag entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }

        public void Mergue(Persistence.Entities.Tag entity)
        {
            entity.Name = Name;
        }
    }
}
