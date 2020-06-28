namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Pages
{
	[Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
	public partial class PostDetailPage : XFToolKit.Mvvm.Pages.ContentPage
    {
		public PostDetailPage(Internet.Persistence.Entities.Post post)
		{
			InitializeComponent();
		}
	}
}