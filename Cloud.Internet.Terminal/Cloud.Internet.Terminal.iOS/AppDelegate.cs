
using Foundation;
using UIKit;
using UserNotifications;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //var tintColor = UIColor.FromRGB(128, 0, 0); // Primary Brand Color
            var tintColor = UIColor.FromRGB(238, 160, 85); // Secoundary Brand Color

            UIButton.Appearance.TintColor = tintColor;
            UISwitch.Appearance.OnTintColor = tintColor;

            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_sqlite3());

            Xamarin.Forms.Forms.SetFlags("MediaElement_Experimental");

            Xamarin.Forms.Forms.Init();

            Xamarin.Forms.PancakeView.iOS.PancakeViewRenderer.Init();

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();

            LoadApplication(new App());

            var result = base.FinishedLaunching(app, options);

            UIApplication.SharedApplication.KeyWindow.TintColor = tintColor;

            return result;
        }
    }
}
