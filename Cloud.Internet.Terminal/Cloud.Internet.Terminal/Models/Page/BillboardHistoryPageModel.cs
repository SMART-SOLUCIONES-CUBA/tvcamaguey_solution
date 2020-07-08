namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Models.Page
{
    public class BillboardHistoryPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        public BillboardHistoryPageModel()
        {
            DayPresentations = new System.Collections.ObjectModel.ObservableCollection<View.DayPresentationsViewModel>();
        }

        public System.Collections.ObjectModel.ObservableCollection<View.DayPresentationsViewModel> DayPresentations { get; set; }

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
