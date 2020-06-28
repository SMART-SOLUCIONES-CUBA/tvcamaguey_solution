namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models
{
    public class ShowDisplayModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Cloud.Persistence.Entities.Show, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string About { get; set; }

        public ImageDisplayModel LogoImage { get; set; }

        public ImageDisplayModel PosterImage { get; set; }

        public void Import(Cloud.Persistence.Entities.Show entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            About = entity.About;
            LogoImage = new ImageDisplayModel();
            PosterImage = new ImageDisplayModel();

            LogoImage.Import(entity.Logo);
            PosterImage.Import(entity.Poster);
        }
    }
}
