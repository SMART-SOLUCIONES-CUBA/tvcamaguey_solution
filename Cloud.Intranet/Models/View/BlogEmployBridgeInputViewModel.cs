namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Models.View
{
    public class BlogEmployBridgeInputViewModel
    {
        public BlogEmployBridgeInputViewModel()
        {
            AvailableEmploys = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
        }

        public System.Guid BlogId { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Employ")]
        public System.Guid EmployId { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AvailableEmploys { get; set; }
    }
}
