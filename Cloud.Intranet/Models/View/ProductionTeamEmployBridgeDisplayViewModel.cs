namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Models.View
{
    public class ProductionTeamEmployBridgeDisplayViewModel
    {
        public System.Guid ProductionTeamId { get; set; }

        public ProductionTeamDisplayViewModel ProductionTeam { get; set; }

        public System.Guid EmployId { get; set; }

        public EmployDisplayViewModel Employ { get; set; }

        public Cloud.Persistence.Enumerations.ProductionRole ProductionRole { get; set; }
    }
}
