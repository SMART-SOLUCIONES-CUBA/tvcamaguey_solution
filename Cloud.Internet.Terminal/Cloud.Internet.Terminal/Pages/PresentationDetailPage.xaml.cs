namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Pages
{
	[Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
	public partial class PresentationDetailPage : XFToolKit.Mvvm.Pages.ContentPage
    {
		public PresentationDetailPage(Cloud.Persistence.Entities.Presentation presentation)
		{
			InitializeComponent();
		}
	}
}