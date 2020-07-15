namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Models.Page
{
    public class ShowDetailPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private System.Collections.Generic.List<Cloud.Persistence.Entities.Presentation> nextPresentations;
        private System.Collections.Generic.List<Cloud.Persistence.Entities.Presentation> oldPresentations;

        public Cloud.Persistence.Entities.Show Show { get; set; }

        public System.Collections.Generic.List<Cloud.Persistence.Entities.Presentation> NextPresentations { get => nextPresentations; set => SetProperty(ref nextPresentations, value); }
        public System.Collections.Generic.List<Cloud.Persistence.Entities.Presentation> OldPresentations { get => oldPresentations; set => SetProperty(ref oldPresentations, value); }
    }
}
