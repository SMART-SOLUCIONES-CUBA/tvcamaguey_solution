[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.UWP.Helpers.SkiaSharpHelper))]

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.UWP.Helpers
{
    public class SkiaSharpHelper : Terminal.Helpers.Facade.ISkiaSharpHelperFacade
    {
        public SkiaSharp.SKTypeface GetSKTypefaceByFontFileName(string fontFileName)
        {
            return SkiaSharp.SKTypeface.FromFile(System.IO.Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, string.Format(@"Assets\Fonts\{0}", fontFileName)));
        }
    }
}