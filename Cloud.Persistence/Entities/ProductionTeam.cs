using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.Entities
{
    public class ProductionTeam : SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        public ProductionTeam()
        {
            Id = System.Guid.NewGuid();
            EmployBridges = new List<ProductionTeamEmployBridge>();
        }

        [Key]
        public System.Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<ProductionTeamEmployBridge> EmployBridges { get; set; }
    }
}
