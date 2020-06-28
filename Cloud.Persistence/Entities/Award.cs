using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.Entities
{
    public class Award : SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        public Award()
        {
            Id = System.Guid.NewGuid();
            EmployBridges = new List<EmployAwardBridge>();
        }

        [Key]
        public System.Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Edition { get; set; }

        public bool IsInstitutional { get; set; }

        public string Description { get; set; }

        public List<EmployAwardBridge> EmployBridges { get; set; }
    }
}
