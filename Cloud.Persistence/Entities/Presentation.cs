namespace SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.Entities
{
    public class Presentation : SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        public Presentation()
        {
            Id = System.Guid.NewGuid();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        public System.DateTime At { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ShowId")]
        public Show Show { get; set; }

        public System.Guid ShowId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ProductionTeamId")]
        public ProductionTeam ProductionTeam { get; set; }

        public System.Guid ProductionTeamId { get; set; }

        public string About { get; set; }
    }
}
