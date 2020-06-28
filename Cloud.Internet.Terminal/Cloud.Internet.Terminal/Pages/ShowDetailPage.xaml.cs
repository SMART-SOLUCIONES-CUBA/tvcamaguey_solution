namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Pages
{
	[Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
	public partial class ShowDetailPage : XFToolKit.Mvvm.Pages.ContentPage
    {
		public ShowDetailPage(Cloud.Persistence.Entities.Show show)
		{
			InitializeComponent();
		}
	}
}