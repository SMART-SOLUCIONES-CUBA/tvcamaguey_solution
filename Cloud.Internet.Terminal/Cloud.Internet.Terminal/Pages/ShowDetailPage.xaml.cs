namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Pages
{
	[Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
	public partial class ShowDetailPage : XFToolKit.Mvvm.Pages.ContentPage, Facade.IImageContextBindingPageFacade
    {
		public ShowDetailPage(Cloud.Persistence.Entities.Show show)
		{
			InitializeComponent();

			BindingContext = show;
		}

        public SAPRESSC.Middleware.Media.Persistence.Entities.Image BindingContextImage => (BindingContext as Cloud.Persistence.Entities.Show)?.Poster;
    }
}