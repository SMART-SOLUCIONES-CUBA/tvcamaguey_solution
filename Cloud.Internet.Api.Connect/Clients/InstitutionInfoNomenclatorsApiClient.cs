namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Connect.Clients
{
    public class InstitutionInfoNomenclatorsApiClient : SAPRESSC.Core.Api.Connect.Clients.AbstractEntityApiManagementClient<Persistence.Entities.InstitutionInfoNomenclator, Persistence.Enumerators.InstitutionInfoKind, Common.Models.InstitutionInfoNomenclatorInputModel, Common.Models.InstitutionInfoNomenclatorDisplayModel>
    {
        public InstitutionInfoNomenclatorsApiClient(SAPRESSC.Core.Api.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        protected override string TargetController => "institutioninfonomenclators";
    }
}
