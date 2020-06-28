namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Persistence.EF.Providers
{
    public class ApplicationDbSchemaConfigurationProvider : SAPRESSC.Core.Persistence.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade
    {
        public string DefaultPersistenceSchema => null;

        public System.Collections.Generic.ICollection<SAPRESSC.Core.Persistence.EF.Configurators.IEntityTypeConfigurator> EntityTypeConfigurators => new System.Collections.Generic.List<SAPRESSC.Core.Persistence.EF.Configurators.IEntityTypeConfigurator>
        {
            new Configurators.PostEntityTypeConfigurator(),
            new Configurators.CategoryEntityTypeConfigurator(),
            new Configurators.TagEntityTypeConfigurator(),
            new Configurators.InstitutionBusinessUpdateRecordEntityTypeConfigurator(),
            new Configurators.InstitutionInfoUpdateRecordEntityTypeConfigurator(),
            new Configurators.InstitutionInfoNomenclatorEntityTypeConfigurator()
        };
    }
}
