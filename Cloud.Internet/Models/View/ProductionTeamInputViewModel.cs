namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View
{
    public class ProductionTeamInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Cloud.Persistence.Entities.ProductionTeam, System.Guid>
    {
        public System.Guid Id { get; set; }

        public Cloud.Persistence.Entities.ProductionTeam Export()
        {
            throw new System.InvalidOperationException();
        }

        public void Import(Cloud.Persistence.Entities.ProductionTeam entity)
        {
            throw new System.InvalidOperationException();
        }

        public void Mergue(Cloud.Persistence.Entities.ProductionTeam entity)
        {
            throw new System.InvalidOperationException();
        }
    }
}
