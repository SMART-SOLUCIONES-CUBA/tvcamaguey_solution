namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Connect.Clients
{
    public class MediFileUploadApiClient
    {
        private readonly SAPRESSC.Core.Api.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider;

        public MediFileUploadApiClient(SAPRESSC.Core.Api.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider)
        {
            this.httpClientHelperProvider = httpClientHelperProvider;
        }

        public virtual async System.Threading.Tasks.Task UploadImageAsync(Common.Models.MediaFileUploadModel model)
        {
            await httpClientHelperProvider.HttpClientHelper.PostAsync("mediafileupload/image", model);
        }
    }
}
