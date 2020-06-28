namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Models.Page
{
    public class NewsPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private int total;
        private bool isLoadingInfinity;

        public NewsPageModel()
        {
            isLoadingInfinity = false;

            Posts = new DLToolkit.Forms.Controls.FlowObservableCollection<View.BasePostViewModel>();
        }

        public DLToolkit.Forms.Controls.FlowObservableCollection<View.BasePostViewModel> Posts { get; set; }

        public int Total { get => total; set => SetProperty(ref total, value); }

        public bool IsLoadingInfinite { get => isLoadingInfinity; set => SetProperty(ref isLoadingInfinity, value); }

        protected override void DisposeUnmanagementResources()
        {
            base.DisposeUnmanagementResources();

            Posts.Clear();

            Posts = null;
        }
    }
}
