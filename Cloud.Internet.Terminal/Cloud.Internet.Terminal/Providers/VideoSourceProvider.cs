using System.Reflection;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Providers
{
    public static class VideoSourceProvider
    {
        public static XFToolKit.Presentation.Sources.Video.VideoSource HomeVideoSource
        {
            get
            {
                if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
                {
                    return XFToolKit.Presentation.Sources.Video.VideoSource.FromResource("home.mp4");
                }

                return XFToolKit.Presentation.Sources.Video.VideoSource.FromResource("Assets/Videos/Home.mp4");
            }
        }
    }
}
