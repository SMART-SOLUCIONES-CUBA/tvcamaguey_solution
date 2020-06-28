using Microsoft.EntityFrameworkCore.ChangeTracking;
using SmartSolucionesCuba.TVCamaguey.Cloud.Syncronization.Handling.Common.Models;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Helpers.Syncronization
{
    public class SyncronizationDbChangeHandleHelper : SAPRESSC.Syncronization.Persistence.EF.Helpers.Facade.ISyncronizationDbChangeHandleHelperFacade
    {
        public object GetSyncronizationModelObject(object entity)
        {
            if (entity is Cloud.Persistence.Entities.Award)
            {
                var model = new AwardSyncModel();

                model.Import(entity as Cloud.Persistence.Entities.Award);

                return model;
            }

            if (entity is Cloud.Persistence.Entities.Blog)
            {
                var model = new BlogSyncModel();

                model.Import(entity as Cloud.Persistence.Entities.Blog);

                return model;
            }

            if (entity is Cloud.Persistence.Entities.EmployAdministrationRoleBridge)
            {
                var model = new EmployAdministrationRoleBridgeSyncModel();

                model.Import(entity as Cloud.Persistence.Entities.EmployAdministrationRoleBridge);

                return model;
            }

            if (entity is Cloud.Persistence.Entities.EmployAwardBridge)
            {
                var model = new EmployAwardBridgeSyncModel();

                model.Import(entity as Cloud.Persistence.Entities.EmployAwardBridge);

                return model;
            }

            if (entity is Cloud.Persistence.Entities.EmployBlogBridge)
            {
                var model = new EmployBlogBridgeSyncModel();

                model.Import(entity as Cloud.Persistence.Entities.EmployBlogBridge);

                return model;
            }

            if (entity is Cloud.Persistence.Entities.Employ)
            {
                var model = new EmploySyncModel();

                model.Import(entity as Cloud.Persistence.Entities.Employ);

                return model;
            }

            if (entity is Cloud.Persistence.Entities.Presentation)
            {
                var model = new PresentationSyncModel();

                model.Import(entity as Cloud.Persistence.Entities.Presentation);

                return model;
            }

            if (entity is Cloud.Persistence.Entities.ProductionTeam)
            {
                var model = new ProductionTeamSyncModel();

                model.Import(entity as Cloud.Persistence.Entities.ProductionTeam);

                return model;
            }

            if (entity is Cloud.Persistence.Entities.ProductionTeamEmployBridge)
            {
                var model = new ProductionTeamEmployBridgeSyncModel();

                model.Import(entity as Cloud.Persistence.Entities.ProductionTeamEmployBridge);

                return model;
            }

            if (entity is Cloud.Persistence.Entities.Show)
            {
                var model = new ShowSyncModel();

                model.Import(entity as Cloud.Persistence.Entities.Show);

                return model;
            }

            throw new System.InvalidOperationException();
        }

        public bool IsSyncronizationTarget(EntityEntry entityEntry)
        {
            var entityType = entityEntry.Entity.GetType();

            return
                entityType.FullName == string.Format("SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.Entities.{0}", entityType.Name);
        }
    }
}
