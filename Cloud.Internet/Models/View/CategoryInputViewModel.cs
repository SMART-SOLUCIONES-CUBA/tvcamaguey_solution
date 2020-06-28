namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View
{
    public class CategoryInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Persistence.Entities.Category, string>
    {
        public CategoryInputViewModel()
        {
            AvailableCategorySelectListItems = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
        }

        public string Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.Display(Name = "Name")]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Parent")]
        public string ParentId { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AvailableCategorySelectListItems { get; set; }

        public Persistence.Entities.Category Export()
        {
            var slugHelper = new Slugify.SlugHelper();

            var entity = new Persistence.Entities.Category
            {
                Id = slugHelper.GenerateSlug(Name)
            };

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.Category entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            ParentId = entity.ParentId;
        }

        public void Mergue(Persistence.Entities.Category entity)
        {
            entity.Name = Name;

            if (entity.ParentId != ParentId)
            {
                entity.ParentId = ParentId;
            }
        }
    }
}
