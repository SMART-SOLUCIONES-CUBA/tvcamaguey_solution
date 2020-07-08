[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Droid.Helpers.ExceptionHandlingHelper))]

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Droid.Helpers
{
    public class ExceptionHandlingHelper : Terminal.Helpers.Facade.IExceptionHandlingHelper
    {
        public System.Exception ToCrossPlatformException(System.Exception exception)
        {
            if (exception is Java.Net.UnknownHostException)
            {
                return new System.Net.WebException(exception.Message, exception);
            }

            return exception;
        }
    }
}
