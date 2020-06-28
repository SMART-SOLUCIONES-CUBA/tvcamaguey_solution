namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View
{
    public class CategoryDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Category, string>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ParentName { get; set; }

        public void Import(Persistence.Entities.Category entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            ParentName = entity.Parent?.Name;
        }
    }
}
