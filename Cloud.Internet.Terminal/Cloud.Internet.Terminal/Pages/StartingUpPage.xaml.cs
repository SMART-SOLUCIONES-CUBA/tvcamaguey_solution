namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class StartingUpPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        public StartingUpPage()
        {
            InitializeComponent();

            BindingContext = new XFToolKit.Mvvm.Models.Page.BasePageModel
            {
                IsBusy = true
            };
        }
    }
}
