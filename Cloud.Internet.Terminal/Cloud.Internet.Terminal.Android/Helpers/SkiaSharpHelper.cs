[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Droid.Helpers.SkiaSharpHelper))]

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Droid.Helpers
{
    public class SkiaSharpHelper : Terminal.Helpers.Facade.ISkiaSharpHelperFacade
    {
        public SkiaSharp.SKTypeface GetSKTypefaceByFontFileName(string fontFileName)
        {
            return SkiaSharp.SKTypeface.FromStream(Android.App.Application.Context.Assets.Open(System.IO.Path.Combine("fonts", fontFileName)));
        }
    }
}