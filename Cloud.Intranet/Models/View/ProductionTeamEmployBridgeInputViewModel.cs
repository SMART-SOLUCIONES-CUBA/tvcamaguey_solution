namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Models.View
{
    public class ProductionTeamEmployBridgeInputViewModel
    {
        public ProductionTeamEmployBridgeInputViewModel()
        {
            AvailableEmploys = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
        }

        public System.Guid ProductionTeamId { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "ProductionRole")]
        public Cloud.Persistence.Enumerations.ProductionRole ProductionRole { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Employ")]
        public System.Guid EmployId { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AvailableEmploys { get; set; }
    }
}
