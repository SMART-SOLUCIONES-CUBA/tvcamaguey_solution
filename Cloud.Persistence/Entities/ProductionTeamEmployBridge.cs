namespace SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.Entities
{
    public class ProductionTeamEmployBridge
    {
        public System.Guid ProductionTeamId { get; set; }

        public ProductionTeam ProductionTeam { get; set; }

        public System.Guid EmployId { get; set; }

        public Employ Employ { get; set; }

        public Enumerations.ProductionRole ProductionRole { get; set; }
    }
}
