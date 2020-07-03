using UIKit;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.iOS.Helpers.SizeCalculationHelper))]

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.iOS.Helpers
{
    public class SizeCalculationHelper : Terminal.Helpers.Facade.ISizeCalculationHelperFacade
    {
        public double CalculateHeight(string text, double width, Xamarin.Forms.Font font)
        {
            var size = NSStringDrawing.GetSizeUsingAttributes(new Foundation.NSString(text), new UIStringAttributes
            {
                Font = font.ToUIFont()
            });

            return size.Height;
        }
    }
}
