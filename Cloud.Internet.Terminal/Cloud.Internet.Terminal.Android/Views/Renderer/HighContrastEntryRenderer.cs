[assembly: Xamarin.Forms.ExportRenderer(typeof(SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Views.Rendered.HighContrastEntry), typeof(SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Droid.Views.Renderer.HighContrastEntryRenderer))]

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Droid.Views.Renderer
{
    public class HighContrastEntryRenderer : Xamarin.Forms.Platform.Android.EntryRenderer
    {
        public HighContrastEntryRenderer(Android.Content.Context context) : base(context)
        {
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Background.SetColorFilter(Android.Graphics.Color.White, Android.Graphics.PorterDuff.Mode.SrcAtop);

                var intPtrtextViewClass = Android.Runtime.JNIEnv.FindClass(typeof(Android.Widget.TextView));

                var mCursorDrawableResProperty = Android.Runtime.JNIEnv.GetFieldID(intPtrtextViewClass, "mCursorDrawableRes", "I");

                Android.Runtime.JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, Resource.Drawable.high_contrast_cursor);
            }            
        }
    }
}