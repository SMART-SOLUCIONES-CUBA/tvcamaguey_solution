
using Android.App;
using Android.OS;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Droid
{
    [Activity(Label = "TuTV Camagüey", Icon = "@mipmap/ic_launcher", Theme = "@style/SplashTheme", ConfigurationChanges = Android.Content.PM.ConfigChanges.ScreenSize | Android.Content.PM.ConfigChanges.Orientation, MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(2)).ContinueWith(_ =>
            {
                StartActivity(new Android.Content.Intent(Application.Context, typeof(MainActivity)));
            });
        }
    }
}
