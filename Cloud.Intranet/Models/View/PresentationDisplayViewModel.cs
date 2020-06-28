using SmartSolucionesCuba.TVCamaguey.Cloud.Common.Extensions;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Models.View
{
    public class PresentationDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Cloud.Persistence.Entities.Presentation, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.DateTime At { get; set; }

        public string ShowName { get; set; }

        public System.Guid ShowLogoId { get; set; }

        public string ShowLogoExtension { get; set; }

        public string ProductionTeamName { get; set; }

        public void Import(Cloud.Persistence.Entities.Presentation entity)
        {
            Id = entity.Id;
            At = entity.At;
            ShowName = entity.Show.Name;
            ShowLogoId = entity.Show.Logo.Id;
            ShowLogoExtension = entity.Show.Logo.ContentType.ContentTypeToExtension();
            ProductionTeamName = entity.ProductionTeam.Name;
        }
    }
}
