namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Connect.Clients
{
    public class ShowsApiClient : SAPRESSC.Core.Api.Connect.Clients.AbstractEntityApiManagementClient<Cloud.Persistence.Entities.Show, System.Guid, Common.Models.ShowInputModel, Common.Models.ShowDisplayModel>
    {
        public ShowsApiClient(SAPRESSC.Core.Api.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        protected override string TargetController => "shows";
    }
}
