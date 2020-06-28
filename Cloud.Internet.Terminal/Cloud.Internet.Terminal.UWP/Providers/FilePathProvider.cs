[assembly: Xamarin.Forms.Dependency(typeof(SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.UWP.Providers.FilePathProvider))]

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.UWP.Providers
{
    public class FilePathProvider : Terminal.Providers.Facade.IFilePathProviderFacade
    {
        public string GetDatabaseFilePath()
        {
            return GetInternalFilePath("cloud.db");
        }

        public string GetDefaultUserProfileImageFilePath(int width, int height)
        {
            return GetInternalFilePath(string.Format("default-user-{0}x{1}.jpg", width, height));
        }

        public string GetImageFilePath(System.Guid imageId, string extension)
        {
            if (extension == null)
            {
                return string.Empty;
            }

            if (extension.StartsWith('.'))
            {
                return GetInternalFilePath(string.Format("{0}{1}", imageId.ToString().ToLowerInvariant(), extension));
            }
            else
            {
                return GetInternalFilePath(string.Format("{0}.{1}", imageId.ToString().ToLowerInvariant(), extension));
            }
        }

        private string GetInternalFilePath(string fileName)
        {
            var documentPath = Windows.Storage.ApplicationData.Current.LocalFolder.Path;

            return System.IO.Path.Combine(documentPath, fileName);
        }
    }
}
