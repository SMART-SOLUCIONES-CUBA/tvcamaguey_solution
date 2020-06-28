namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Models.View
{
    public class ProductionTeamMembersManageViewModel
    {
        public ProductionTeamMembersManageViewModel()
        {
            Members = new System.Collections.Generic.List<ProductionTeamEmployBridgeDisplayViewModel>();
            AvailableEmploys = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
        }

        public ProductionTeamDisplayViewModel ProductionTeam { get; set; }

        public System.Collections.Generic.List<ProductionTeamEmployBridgeDisplayViewModel> Members { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AvailableEmploys { get; set; }
    }
}
