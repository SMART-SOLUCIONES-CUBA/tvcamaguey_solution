[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.UWP.Helpers.ExceptionHandlingHelper))]

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.UWP.Helpers
{
    public class ExceptionHandlingHelper : Terminal.Helpers.Facade.IExceptionHandlingHelper
    {
        public System.Exception ToCrossPlatformException(System.Exception exception)
        {
            return exception;
        }
    }
}
