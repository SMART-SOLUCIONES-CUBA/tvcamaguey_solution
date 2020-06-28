using SmartSolucionesCuba.TVCamaguey.Cloud.Common.Extensions;
using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View
{
    public class EmployDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Cloud.Persistence.Entities.Employ, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Alias { get; set; }

        public System.DateTime BornDate { get; set; }

        public SAPRESSC.Middleware.Identity.Persistence.Enumerations.CivilStatus CivilStatus { get; set; }

        public SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex Sex { get; set; }

        public string About { get; set; }

        public System.Guid ImageId { get; set; }

        public string ImageExtension { get; set; }

        public Cloud.Persistence.Enumerations.AdministrativeRole[] AdministrativeRoles { get; set; } 

        public void Import(Cloud.Persistence.Entities.Employ entity)
        {
            Id = entity.Id;
            Name = entity.Identity.Name;
            LastName = entity.Identity.LastName;
            Alias = entity.Identity.Alias;
            BornDate = entity.Identity.BornDate;
            CivilStatus = entity.Identity.CivilStatus;
            Sex = entity.Identity.Sex;
            About = entity.Identity.About;
            ImageId = entity.Identity.ImageId ?? System.Guid.Empty;
            AdministrativeRoles = entity.AdministrationRoleBridges.Select(b => b.AdministrativeRole).ToArray();

            if (entity.Identity.ImageId != null && entity.Identity.ImageId != System.Guid.Empty)
            {
                ImageExtension = entity.Identity.Image.ContentType.ContentTypeToExtension();
            }
        }
    }
}
