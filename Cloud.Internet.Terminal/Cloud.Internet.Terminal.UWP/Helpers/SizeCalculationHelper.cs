using Xamarin.Forms.Platform.UWP;

[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.UWP.Helpers.SizeCalculationHelper))]

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.UWP.Helpers
{
    public class SizeCalculationHelper : Terminal.Helpers.Facade.ISizeCalculationHelperFacade
    {
        public double CalculateHeight(string text, double width, Xamarin.Forms.Font font)
        {
            var textBlock = new Windows.UI.Xaml.Controls.TextBlock();

            textBlock.ApplyFont(font);
            textBlock.Text = text;

            textBlock.MaxHeight = double.PositiveInfinity;
            textBlock.MaxWidth = width;

            var parentBorder = new Windows.UI.Xaml.Controls.Border { Child = textBlock };

            parentBorder.Measure(new Windows.Foundation.Size(textBlock.MaxWidth, textBlock.MaxHeight));
            parentBorder.Child = null;

            return (int)parentBorder.DesiredSize.Height;
        }
    }
}
