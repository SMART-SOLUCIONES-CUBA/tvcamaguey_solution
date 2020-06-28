namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View
{
    public class ProductionTeamMembersViewModel
    {
        public ProductionTeamMembersViewModel()
        {
            Members = new System.Collections.Generic.List<ProductionTeamEmployBridgeDisplayViewModel>();
        }

        public ProductionTeamDisplayViewModel ProductionTeam { get; set; }

        public System.Collections.Generic.List<ProductionTeamEmployBridgeDisplayViewModel> Members { get; set; }
    }
}
