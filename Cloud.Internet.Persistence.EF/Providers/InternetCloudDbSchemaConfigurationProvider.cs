namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.EF.Providers
{
    public class InternetCloudDbSchemaConfigurationProvider : SAPRESSC.Core.Persistence.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade
    {
        public string DefaultPersistenceSchema => null;

        public System.Collections.Generic.ICollection<SAPRESSC.Core.Persistence.EF.Configurators.IEntityTypeConfigurator> EntityTypeConfigurators => new System.Collections.Generic.List<SAPRESSC.Core.Persistence.EF.Configurators.IEntityTypeConfigurator>
        {
            new Configurators.PostTagBridgeEntityTypeConfigurator()
        };
    }
}
