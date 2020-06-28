namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models
{
    public class CategoryDisplayModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Category, string>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public CategoryDisplayModel Parent { get; set; }

        public void Import(Persistence.Entities.Category entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            
            if (entity.Parent != null)
            {
                Parent = new CategoryDisplayModel();

                Parent.Import(entity.Parent);
            }
        }
    }
}
