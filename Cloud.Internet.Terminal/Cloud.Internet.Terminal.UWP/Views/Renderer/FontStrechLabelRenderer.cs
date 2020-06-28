[assembly: Xamarin.Forms.Platform.UWP.ExportRenderer(typeof(SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Views.Rendered.FontStrechLabel), typeof(SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.UWP.Views.Renderer.FontStrechLabelRenderer))]

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.UWP.Views.Renderer
{
    public class FontStrechLabelRenderer : Xamarin.Forms.Platform.UWP.LabelRenderer
    {
        protected override void OnElementChanged(Xamarin.Forms.Platform.UWP.ElementChangedEventArgs<Xamarin.Forms.Label> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            }
        }
    }
}
