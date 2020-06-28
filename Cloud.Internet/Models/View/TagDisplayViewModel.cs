namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View
{
    public class TagDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Tag, string>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void Import(Persistence.Entities.Tag entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
    }
}
