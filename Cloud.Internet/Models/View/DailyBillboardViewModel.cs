namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View
{
    public class DailyBillboardViewModel
    {
        public DailyBillboardViewModel()
        {
            Presentations = new System.Collections.Generic.List<PresentationDisplayViewModel>();
        }

        public System.DateTime At { get; set; }

        public System.Collections.Generic.List<PresentationDisplayViewModel> Presentations { get; set; }
    }
}
