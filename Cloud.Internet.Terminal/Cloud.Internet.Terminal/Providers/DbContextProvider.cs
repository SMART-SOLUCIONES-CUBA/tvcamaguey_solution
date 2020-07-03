using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Providers
{
    public sealed class DbContextProvider
    {
        private static DbContextProvider instance;

        public static DbContextProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DbContextProvider();
                }

                return instance;
            }
        }

        private DbContextProvider() { }

        public Persistence.EF.Context.ApplicationDbContext GetDbContext()
        {
            var optionBuilder = new DbContextOptionsBuilder();

            optionBuilder.UseSqlite(string.Format("Filename={0}", Helpers.LocalFileStorageHelper.GetDatabaseFilePath()));

            var syncronizationDbChangeHandleHelpers = new SAPRESSC.Syncronization.Persistence.EF.Helpers.Facade.ISyncronizationDbChangeHandleHelperFacade[]
            {
                new SAPRESSC.Middleware.Media.Syncronization.Handling.Persistence.EF.Helpers.MediaSyncronizationDbChangeHandleHelper(),
                new SAPRESSC.Middleware.Region.Syncronization.Handling.Persistence.EF.Helpers.RegionSyncronizationDbChangeHandleHelper(),
                new SAPRESSC.Middleware.Identity.Syncronization.Handling.Persistence.EF.Helpers.IdentitySyncronizationDbChangeHandleHelper()
            };

            var dbSchemaConfigurationProviders = new SAPRESSC.Core.Persistence.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade[]
            {
                new SAPRESSC.Middleware.Media.Persistence.EF.Providers.MediaMiddlewareDbSchemaConfigurationProvider(),
                new SAPRESSC.Middleware.Region.Persistence.EF.Providers.RegionMiddlewareDbSchemaConfigurationProvider(),
                new SAPRESSC.Middleware.Identity.Persistence.EF.Providers.IdentityMiddlewareDbSchemaConfigurationProvider(),
                new SAPRESSC.Syncronization.Persistence.EF.Providers.SyncronizationDbSchemaConfigurationProvider(),
                new Cloud.Persistence.EF.Providers.BusinessDbSchemaConfigurationProvider(),
                new Internet.Persistence.EF.Providers.InternetCloudDbSchemaConfigurationProvider(),
                new Persistence.EF.Providers.ApplicationDbSchemaConfigurationProvider()
            };

            return new Persistence.EF.Context.ApplicationDbContext(syncronizationDbChangeHandleHelpers, dbSchemaConfigurationProviders, optionBuilder.Options);
        }

        public async System.Threading.Tasks.Task InitializeAsync(Persistence.EF.Context.ApplicationDbContext dbContext)
        {
            await dbContext.Database.EnsureCreatedAsync();
        }
    }
}
