namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Models.Page
{
    public class HomePageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private System.Collections.Generic.List<View.BasePostViewModel> posts;
        private System.Collections.Generic.List<Cloud.Persistence.Entities.Presentation> presentations;

        public System.Collections.Generic.List<View.BasePostViewModel> Posts { get => posts; set => SetProperty(ref posts, value); }        

        public System.Collections.Generic.List<Cloud.Persistence.Entities.Presentation> Presentations { get => presentations; set => SetProperty(ref presentations, value); }
    }
}
