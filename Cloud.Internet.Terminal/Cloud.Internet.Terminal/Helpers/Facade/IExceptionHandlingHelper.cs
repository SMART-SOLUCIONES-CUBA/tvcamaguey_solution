namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Helpers.Facade
{
    public interface IExceptionHandlingHelper
    {
        System.Exception ToCrossPlatformException(System.Exception exception);
    }
}
