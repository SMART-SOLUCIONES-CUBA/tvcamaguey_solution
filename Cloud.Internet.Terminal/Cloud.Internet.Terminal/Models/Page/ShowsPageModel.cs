namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Models.Page
{
    public class ShowsPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private DLToolkit.Forms.Controls.FlowObservableCollection<Cloud.Persistence.Entities.Show> shows;

        public DLToolkit.Forms.Controls.FlowObservableCollection<Cloud.Persistence.Entities.Show> Shows { get => shows; set => SetProperty(ref shows, value); }

        protected override void DisposeUnmanagementResources()
        {
            base.DisposeUnmanagementResources();

            Shows.Clear();

            Shows = null;
        }
    }
}
