using SmartSolucionesCuba.SAPRESSC.Core.Api.Connect.Providers.Facade;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Connect.Clients
{
    public class PersonIdentitiesApiClient : SAPRESSC.Core.Api.Connect.Clients.AbstractEntityApiManagementClient<SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity, System.Guid, PersonIdentityInputModel, PersonIdentityDisplayModel>
    {
        public PersonIdentitiesApiClient(IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        protected override string TargetController => "personidentities";
    }
}
