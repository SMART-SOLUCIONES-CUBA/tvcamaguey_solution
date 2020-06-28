namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View
{
    public class ProductionTeamDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Cloud.Persistence.Entities.ProductionTeam, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int MembersCount { get; set; }

        public void Import(Cloud.Persistence.Entities.ProductionTeam entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            MembersCount = entity.EmployBridges.Count;
        }
    }
}
