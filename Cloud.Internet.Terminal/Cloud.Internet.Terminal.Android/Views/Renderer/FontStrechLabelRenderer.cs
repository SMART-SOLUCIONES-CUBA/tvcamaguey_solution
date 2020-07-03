[assembly: Xamarin.Forms.ExportRenderer(typeof(SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Views.Rendered.FontStrechLabel), typeof(SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Droid.Views.Renderer.FontStrechLabelRenderer))]

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Droid.Views.Renderer
{
    public class FontStrechLabelRenderer : Xamarin.Forms.Platform.Android.LabelRenderer
    {
        public FontStrechLabelRenderer(Android.Content.Context context) : base(context)
        {
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Xamarin.Forms.Label> e)
        {
            base.OnElementChanged(e);

            Control?.SetIncludeFontPadding(false);
            Control?.SetPadding(0, (int)Android.App.Application.Context.Resources.DisplayMetrics.ScaledDensity * 1, 0, (int)Android.App.Application.Context.Resources.DisplayMetrics.ScaledDensity * 1);
        }
    }
}