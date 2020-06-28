namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Models.Page
{
    public class ShowsPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        public ShowsPageModel()
        {
            Shows = new DLToolkit.Forms.Controls.FlowObservableCollection<Cloud.Persistence.Entities.Show>();
        }

        public DLToolkit.Forms.Controls.FlowObservableCollection<Cloud.Persistence.Entities.Show> Shows { get; private set; }

        protected override void DisposeUnmanagementResources()
        {
            base.DisposeUnmanagementResources();

            Shows.Clear();

            Shows = null;
        }
    }
}
