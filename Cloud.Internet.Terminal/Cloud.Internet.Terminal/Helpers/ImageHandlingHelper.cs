using SmartSolucionesCuba.TVCamaguey.Cloud.Common.Extensions;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Helpers
{
    public static class ImageHandlingHelper
    {
        public static async System.Threading.Tasks.Task FetchRemoteImageFileAsync(System.Guid id, string contentType)
        {
            var filePathProvider = Xamarin.Forms.DependencyService.Get<Providers.Facade.IFilePathProviderFacade>();

            var imageFilePath = filePathProvider.GetImageFilePath(id, contentType.ContentTypeToExtension());

            if (System.IO.File.Exists(imageFilePath)) return;

            var imageRequest = System.Net.WebRequest.Create(string.Format("{0}/media/images/{1}.{2}", Providers.HttpClientHelperProvider.CLOUD_SERVER_BASE_URI, id, contentType.ContentTypeToExtension()));

            imageRequest.Method = System.Net.WebRequestMethods.Http.Get;

            var imageResponse = imageRequest.GetResponse();

            using (var imageFileStream = new System.IO.FileStream(imageFilePath, System.IO.FileMode.Create))
            {
                using (var imageResponseStream = imageResponse.GetResponseStream())
                {
                    await imageResponseStream.CopyToAsync(imageFileStream);
                }
            }
        }

        public static void RemoveLocalImageFile(System.Guid id, string contentType)
        {
            var filePathProvider = Xamarin.Forms.DependencyService.Get<Providers.Facade.IFilePathProviderFacade>();

            var imageFilePath = filePathProvider.GetImageFilePath(id, contentType.ContentTypeToExtension());

            System.IO.File.Delete(imageFilePath);
        }
    }
}
