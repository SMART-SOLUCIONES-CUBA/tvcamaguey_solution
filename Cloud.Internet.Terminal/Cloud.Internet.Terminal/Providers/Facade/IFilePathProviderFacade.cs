namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Providers.Facade
{
    public interface IFilePathProviderFacade
    {
        string GetDatabaseFilePath();

        string GetImageFilePath(System.Guid imageId, string extension);

        string GetDefaultUserProfileImageFilePath(int width, int height);
    }
}
