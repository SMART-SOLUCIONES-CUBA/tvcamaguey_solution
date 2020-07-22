namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Models
{
    public class NavigationSectionModel
    {
        public NavigationSectionModel()
        {
            FontFamily = Xamarin.Forms.Device.RuntimePlatform != Xamarin.Forms.Device.UWP ? "Material Design Icons" : "/Assets/Fonts/materialdesignicons-webfont.ttf#Material Design Icons";
        }

        public string Glyph { get; set; }

        public string FontFamily { get; set; }

        public string Title { get; set; }

        public System.Func<Xamarin.Forms.Page> SelectionFunc { get; set; }
    }
}
