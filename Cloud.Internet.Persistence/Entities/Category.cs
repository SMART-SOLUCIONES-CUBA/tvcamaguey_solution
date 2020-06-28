namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.Entities
{
    public class Category : SAPRESSC.Core.Persistence.Entities.INamedEntity<string>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ParentId")]
        public Category Parent { get; set; }

        public string ParentId { get; set; }
    }
}
