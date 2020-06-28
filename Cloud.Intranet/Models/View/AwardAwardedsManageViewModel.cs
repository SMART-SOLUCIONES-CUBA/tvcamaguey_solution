namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Models.View
{
    public class AwardAwardedsManageViewModel
    {
        public AwardAwardedsManageViewModel()
        {
            Awardeds = new System.Collections.Generic.List<EmployDisplayViewModel>();
            AvailableEmploys = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
        }

        public AwardDisplayViewModel Award { get; set; }

        public System.Collections.Generic.List<EmployDisplayViewModel> Awardeds { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AvailableEmploys { get; set; }
    }
}
