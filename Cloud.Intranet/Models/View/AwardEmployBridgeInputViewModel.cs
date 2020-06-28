namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Models.View
{
    public class AwardEmployBridgeInputViewModel
    {
        public AwardEmployBridgeInputViewModel()
        {
            AvailableEmploys = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
        }

        public System.Guid AwardId { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Employ")]
        public System.Guid EmployId { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AvailableEmploys { get; set; }
    }
}
