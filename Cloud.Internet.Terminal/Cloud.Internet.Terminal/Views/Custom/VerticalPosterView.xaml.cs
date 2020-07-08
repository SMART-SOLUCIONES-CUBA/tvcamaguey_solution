namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Views.Custom
{
    public partial class VerticalPosterView : Xamarin.Forms.ContentView
    {
        public VerticalPosterView(SAPRESSC.Middleware.Media.Persistence.Entities.Image image, double heightRequest)
        {
            InitializeComponent();

            BindingContext = image;

            HeightRequest = heightRequest;
        }
    }
}
