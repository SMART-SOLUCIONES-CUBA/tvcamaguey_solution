namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Models
{
    public class NavigationSectionModel
    {
        public string Glyph { get; set; }

        public string Title { get; set; }

        public System.Func<Xamarin.Forms.Page> SelectionFunc { get; set; }
    }
}
