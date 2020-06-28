using SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Configurators;
using System.Collections.Generic;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.EF.Providers
{
    public class DbSchemaConfigurationProvider : SAPRESSC.Core.Persistence.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade
    {
        public string DefaultPersistenceSchema => null;

        public ICollection<IEntityTypeConfigurator> EntityTypeConfigurators => new List<IEntityTypeConfigurator>
        {
            new Configurators.TerminalUserEntityTypeConfigurator()
        };
    }
}
