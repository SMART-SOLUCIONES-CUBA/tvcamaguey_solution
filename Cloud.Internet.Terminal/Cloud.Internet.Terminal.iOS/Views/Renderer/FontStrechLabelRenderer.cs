[assembly: Xamarin.Forms.ExportRenderer(typeof(SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Views.Rendered.FontStrechLabel), typeof(SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.iOS.Views.Renderer.FontStrechLabelRenderer))]

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.iOS.Views.Renderer
{
    public class FontStrechLabelRenderer : Xamarin.Forms.Platform.iOS.LabelRenderer
    {
        protected override void OnElementChanged(Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<Xamarin.Forms.Label> e)
        {
            base.OnElementChanged(e);

            if (Control == null) return;

            Control.AdjustsFontForContentSizeCategory = true;
            Control.BaselineAdjustment = UIKit.UIBaselineAdjustment.None;
        }
    }
}
