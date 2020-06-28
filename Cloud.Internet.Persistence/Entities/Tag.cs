namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.Entities
{
    public class Tag : SAPRESSC.Core.Persistence.Entities.INamedEntity<string>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
    }
}
