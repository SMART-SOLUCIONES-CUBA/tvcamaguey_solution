using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Connect.Clients
{
    public class PresentationsApiClient : SAPRESSC.Core.Api.Connect.Clients.AbstractEntityApiManagementFilteredClient<Cloud.Persistence.Entities.Presentation, System.Guid, PresentationInputModel, PresentationDisplayModel, Common.Models.Filter.PresentationFilterModel>
    {
        public PresentationsApiClient(SAPRESSC.Core.Api.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        protected override string TargetController => "presentations";
    }
}
