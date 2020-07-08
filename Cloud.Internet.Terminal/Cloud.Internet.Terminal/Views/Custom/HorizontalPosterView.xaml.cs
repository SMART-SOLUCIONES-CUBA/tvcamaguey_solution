namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Views.Custom
{
    public partial class HorizontalPosterView : Xamarin.Forms.ContentView
    {
        public HorizontalPosterView(SAPRESSC.Middleware.Media.Persistence.Entities.Image image, double heightRequest)
        {
            InitializeComponent();

            BindingContext = image;

            HeightRequest = heightRequest;
        }
    }
}
