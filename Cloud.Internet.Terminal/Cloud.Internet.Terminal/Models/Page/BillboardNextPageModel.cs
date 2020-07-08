namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Models.Page
{
    public class BillboardNextPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private System.Collections.ObjectModel.ObservableCollection<View.DayPresentationsViewModel> dayPresentations;

        public System.Collections.ObjectModel.ObservableCollection<View.DayPresentationsViewModel> DayPresentations { get => dayPresentations; set => SetProperty(ref dayPresentations, value); }

        protected override void DisposeUnmanagementResources()
        {
            base.DisposeUnmanagementResources();

            foreach (var dayPresentation in DayPresentations)
            {
                dayPresentation.Clear();
            }

            DayPresentations.Clear();

            DayPresentations = null;
        }
    }
}
