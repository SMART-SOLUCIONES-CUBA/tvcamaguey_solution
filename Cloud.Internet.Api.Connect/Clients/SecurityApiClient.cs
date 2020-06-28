namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Connect.Clients
{
    public class SecurityApiClient
    {
        private readonly SAPRESSC.Core.Api.Connect.Helpers.HttpClientHelper httpClientHelper;

        public SecurityApiClient(SAPRESSC.Core.Api.Connect.Helpers.HttpClientHelper httpClientHelper)
        {
            this.httpClientHelper = httpClientHelper;
        }

        public async System.Threading.Tasks.Task<Common.Models.AuthenticationResponseModel> AuthenticateAsync(Common.Models.AuthenticationRequestModel model)
        {
            return await httpClientHelper.PostAsync<Common.Models.AuthenticationRequestModel, Common.Models.AuthenticationResponseModel>("security/authenticate", model);
        }

        public async System.Threading.Tasks.Task<string> RegisterAsync(Common.Models.RegistrationRequestModel model)
        {
            return await httpClientHelper.PostAsync<Common.Models.RegistrationRequestModel, string>("security/register", model);
        }
    }
}
