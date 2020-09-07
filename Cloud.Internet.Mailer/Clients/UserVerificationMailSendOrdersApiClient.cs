using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Mailer.Api.Common.Models;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Mailer.Clients
{
    public class UserVerificationMailSendOrdersApiClient : SAPRESSC.Core.Api.Connect.Clients.AbstractEntityApiManagementClient<Persistence.Entities.UserVerificationMailSendOrder, System.Guid, UserVerificationMailSendOrderInputModel, UserVerificationMailSendOrderDisplayModel>
    {
        public UserVerificationMailSendOrdersApiClient(SAPRESSC.Core.Api.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        protected override string TargetController => "userverificationmailsendorders";
    }
}
