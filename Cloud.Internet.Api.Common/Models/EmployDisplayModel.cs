using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models
{
    public class EmployDisplayModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Cloud.Persistence.Entities.Employ, System.Guid>
    {
        public System.Guid Id { get; set; }

        public PersonIdentityDisplayModel Identity { get; set; }

        public Cloud.Persistence.Enumerations.AdministrativeRole[] AdministrativeRoles { get; set; }

        public void Import(Cloud.Persistence.Entities.Employ entity)
        {
            Id = entity.Id;
            Identity = new PersonIdentityDisplayModel();
            AdministrativeRoles = entity.AdministrationRoleBridges.Select(b => b.AdministrativeRole).ToArray();

            Identity.Import(entity.Identity);
        }
    }
}
