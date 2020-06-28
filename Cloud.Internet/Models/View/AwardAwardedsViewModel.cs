namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View
{
    public class AwardAwardedsViewModel
    {
        public AwardAwardedsViewModel()
        {
            Awardeds = new System.Collections.Generic.List<EmployDisplayViewModel>();
        }

        public AwardDisplayViewModel Award { get; set; }

        public System.Collections.Generic.List<EmployDisplayViewModel> Awardeds { get; set; }
    }
}
