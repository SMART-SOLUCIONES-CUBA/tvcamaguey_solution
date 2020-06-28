namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Connect.Clients
{
    public class EmployeesApiClient : SAPRESSC.Core.Api.Connect.Clients.AbstractEntityApiManagementClient<Cloud.Persistence.Entities.Employ, System.Guid, Common.Models.EmployInputModel, Common.Models.EmployDisplayModel>
    {
        public EmployeesApiClient(SAPRESSC.Core.Api.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        protected override string TargetController => "employees";
    }
}