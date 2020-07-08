namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Models.Page
{
    public class NewsPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private int total;
        private bool isLoadingInfinity;
        private DLToolkit.Forms.Controls.FlowObservableCollection<View.BasePostViewModel> posts;

        public NewsPageModel()
        {
            isLoadingInfinity = false;
        }

        public DLToolkit.Forms.Controls.FlowObservableCollection<View.BasePostViewModel> Posts { get => posts; set => SetProperty(ref posts, value); }

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
