namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Models
{
    public class NavigationSectionModel
    {
        public NavigationSectionModel()
        {
            FontFamily = "Material Design Icons";
        }

        public string Glyph { get; set; }

        public string FontFamily { get; set; }

        public string Title { get; set; }

        public System.Func<Xamarin.Forms.Page> SelectionFunc { get; set; }
    }
}
