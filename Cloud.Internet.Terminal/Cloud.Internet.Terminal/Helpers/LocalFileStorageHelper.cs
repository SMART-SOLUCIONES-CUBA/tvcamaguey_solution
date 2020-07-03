namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Helpers
{
    internal static class LocalFileStorageHelper
    {
        internal static string GetDatabaseFilePath()
        {
            return GetFilePath("cloud.db");
        }

        internal static string GetFilePath(string fileName)
        {
            return System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), fileName);
        }

        internal static string GetDefaultUserProfileImageFilePath(int width, int height)
        {
            return GetFilePath(string.Format("default-user-{0}x{1}.jpg", width, height));
        }

        internal static string GetImageFilePath(System.Guid imageId, string extension)
        {
            if (extension == null)
            {
                return string.Empty;
            }

            if (extension.StartsWith("."))
            {
                return GetFilePath(string.Format("{0}{1}", imageId.ToString().ToLowerInvariant(), extension));
            }
            else
            {
                return GetFilePath(string.Format("{0}.{1}", imageId.ToString().ToLowerInvariant(), extension));
            }
        }
    }
}
