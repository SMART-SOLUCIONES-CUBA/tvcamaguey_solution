namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View
{
    public class BlogAutorsViewModel
    {
        public BlogAutorsViewModel()
        {
            Authors = new System.Collections.Generic.List<EmployDisplayViewModel>();
        }

        public BlogDisplayViewModel Blog { get; set; }

        public System.Collections.Generic.List<EmployDisplayViewModel> Authors { get; set; }
    }
}
