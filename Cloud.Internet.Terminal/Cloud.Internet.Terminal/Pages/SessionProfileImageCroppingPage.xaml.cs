namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class SessionProfileImageCroppingPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        private System.Action<SkiaSharp.SKBitmap> callbackAction;

        private Views.Custom.ImageCropperCanvasView imageCropperCanvasView;

        public SessionProfileImageCroppingPage(SkiaSharp.SKBitmap target, System.Action<SkiaSharp.SKBitmap> callbackAction)
        {
            InitializeComponent();

            this.callbackAction = callbackAction;

            imageCropperCanvasView = new Views.Custom.ImageCropperCanvasView(target, 1f);

            CropCanvasWrapper.Children.Add(imageCropperCanvasView);
        }

        private void ApplyToolbarItem_Activated(object sender, System.EventArgs args)
        {
            callbackAction.Invoke(imageCropperCanvasView.CroppedBitmap);

            callbackAction = null;
        }

        private void CancelToolbarItem_Activated(object sender, System.EventArgs args)
        {
            callbackAction.Invoke(null);

            callbackAction = null;
        }

        private void RotateToLeftButton_Clicked(object sender, System.EventArgs args)
        {
            imageCropperCanvasView.RotateToLeft();
        }

        private void RotateToRightButton_Clicked(object sender, System.EventArgs args)
        {
            imageCropperCanvasView.RotateToRight();
        }

        protected override void DisposeUnmanagementResources()
        {
            base.DisposeUnmanagementResources();

            imageCropperCanvasView = null;
        }
    }
}
