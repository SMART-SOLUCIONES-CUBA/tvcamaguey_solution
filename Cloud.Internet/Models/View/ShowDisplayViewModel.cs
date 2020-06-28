using SmartSolucionesCuba.TVCamaguey.Cloud.Common.Extensions;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View
{
    public class ShowDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Cloud.Persistence.Entities.Show, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public System.Guid PosterId { get; set; }

        public string PosterExtension { get; set; }

        public System.Guid LogoId { get; set; }

        public string LogoExtension { get; set; }

        public string About { get; set; }

        public void Import(Cloud.Persistence.Entities.Show entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            PosterId = entity.Poster.Id;
            PosterExtension = entity.Poster.ContentType.ContentTypeToExtension();
            LogoId = entity.Logo.Id;
            LogoExtension = entity.Logo.ContentType.ContentTypeToExtension();
            About = entity.About;
        }
    }
}
