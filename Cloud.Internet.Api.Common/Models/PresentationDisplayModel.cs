namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models
{
    public class PresentationDisplayModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Cloud.Persistence.Entities.Presentation, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.DateTime At { get; set; }

        public ShowDisplayModel Show { get; set; }

        public ProductionTeamDisplayModel ProductionTeam { get; set; }

        public void Import(Cloud.Persistence.Entities.Presentation entity)
        {
            Id = entity.Id;
            At = entity.At;
            Show = new ShowDisplayModel();
            ProductionTeam = new ProductionTeamDisplayModel();

            Show.Import(entity.Show);
            ProductionTeam.Import(entity.ProductionTeam);
        }
    }
}
