namespace SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.Entities
{
    public class Show : SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        public Show()
        {
            Id = System.Guid.NewGuid();

        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("PosterId")]
        public SAPRESSC.Middleware.Media.Persistence.Entities.Image Poster { get; set; }

        public System.Guid PosterId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("LogoId")]
        public SAPRESSC.Middleware.Media.Persistence.Entities.Image Logo { get; set; }

        public System.Guid LogoId { get; set; }

        public string About { get; set; }
    }
}
