namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Helpers.Facade
{
    // Implementaciones tomadas de https://forums.xamarin.com/discussion/67545/how-to-calculate-or-measure-width-of-a-string

    public interface ISizeCalculationHelperFacade
    {
        double CalculateHeight(string text, double width, Xamarin.Forms.Font font);
    }
}
