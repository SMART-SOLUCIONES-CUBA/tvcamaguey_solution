using SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Configurators;
using System.Collections.Generic;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.EF.Providers
{
    public class BusinessDbSchemaConfigurationProvider : SAPRESSC.Core.Persistence.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade
    {
        public string DefaultPersistenceSchema => null;

        public ICollection<IEntityTypeConfigurator> EntityTypeConfigurators => new List<IEntityTypeConfigurator>
        {
            new Configurators.EmployEntityTypeConfigurator(),
            new Configurators.EmployAdministrationRoleBridgeEntityTypeConfigurator(),
            new Configurators.EmployAwardBridgeEntityTypeConfigurator(),
            new Configurators.EmployBlogBridgeEntityTypeConfigurator(),
            new Configurators.ProductionTeamEmployBridgeEntityTypeConfigurator()
        };
    }
}
