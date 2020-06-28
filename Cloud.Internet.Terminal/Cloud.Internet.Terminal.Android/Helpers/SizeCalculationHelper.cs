[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Droid.Helpers.SizeCalculationHelper))]

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Droid.Helpers
{
    public class SizeCalculationHelper : Terminal.Helpers.Facade.ISizeCalculationHelperFacade
    {
        public double CalculateHeight(string text, double width, Xamarin.Forms.Font font)
        {
            var textPaint = new Android.Text.TextPaint
            {
                AntiAlias = true,
                TextSize = (float)(font.UseNamedSize ? Xamarin.Forms.Device.GetNamedSize(font.NamedSize, typeof(Xamarin.Forms.Label)) : font.FontSize)
            };

            var layout = new Android.Text.StaticLayout(text, textPaint, (int)width, Android.Text.Layout.Alignment.AlignNormal, 1, 0, false);

            return layout.Height;
        }
    }
}