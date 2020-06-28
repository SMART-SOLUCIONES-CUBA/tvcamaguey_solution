namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Models.View
{
    public class BlogAutorsManageViewModel
    {
        public BlogAutorsManageViewModel()
        {
            Authors = new System.Collections.Generic.List<EmployDisplayViewModel>();
            AvailableEmploys = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
        }

        public BlogDisplayViewModel Blog { get; set; }

        public System.Collections.Generic.List<EmployDisplayViewModel> Authors { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AvailableEmploys { get; set; }
    }
}
