using System.Collections.Generic;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.Entities
{
    public class Employ : SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        public Employ()
        {
            Id = System.Guid.NewGuid();
            AdministrationRoleBridges = new List<EmployAdministrationRoleBridge>();
            AwardBridges = new List<EmployAwardBridge>();
            BlogBridges = new List<EmployBlogBridge>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        public SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity Identity { get; set; }

        public System.Guid IdentityId { get; set; }

        public List<EmployAdministrationRoleBridge> AdministrationRoleBridges { get; set; }

        public List<EmployAwardBridge> AwardBridges { get; set; }

        public List<EmployBlogBridge> BlogBridges { get; set; }
    }
}
