using SmartSolucionesCuba.TVCamaguey.Cloud.Syncronization.Handling.Common.Models;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Handlers.Syncronization
{
    public class BusinessEntityEntryChangeHandler : SAPRESSC.Syncronization.Handling.Reception.EF.Handlers.AbstractEntityEntryChangeHandler
    {
        public BusinessEntityEntryChangeHandler(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override bool TryHandleEntryChange(SAPRESSC.Syncronization.Api.Common.Models.EntryChangeModel entryChangeModel)
        {
            if (entryChangeModel.SerializationTargetClass == typeof(Cloud.Persistence.Entities.Award).FullName)
            {
                return HandleEntryChange<Cloud.Persistence.Entities.Award, AwardSyncModel>(entryChangeModel);
            }

            if (entryChangeModel.SerializationTargetClass == typeof(Cloud.Persistence.Entities.Blog).FullName)
            {
                return HandleEntryChange<Cloud.Persistence.Entities.Blog, BlogSyncModel>(entryChangeModel);
            }

            if (entryChangeModel.SerializationTargetClass == typeof(Cloud.Persistence.Entities.Employ).FullName)
            {
                return HandleEntryChange<Cloud.Persistence.Entities.Employ, EmploySyncModel>(entryChangeModel);
            }

            if (entryChangeModel.SerializationTargetClass == typeof(Cloud.Persistence.Entities.EmployAdministrationRoleBridge).FullName)
            {
                return HandleEntryChange<Cloud.Persistence.Entities.EmployAdministrationRoleBridge, EmployAdministrationRoleBridgeSyncModel>(entryChangeModel);
            }

            if (entryChangeModel.SerializationTargetClass == typeof(Cloud.Persistence.Entities.EmployAwardBridge).FullName)
            {
                return HandleEntryChange<Cloud.Persistence.Entities.EmployAwardBridge, EmployAwardBridgeSyncModel>(entryChangeModel);
            }

            if (entryChangeModel.SerializationTargetClass == typeof(Cloud.Persistence.Entities.EmployBlogBridge).FullName)
            {
                return HandleEntryChange<Cloud.Persistence.Entities.EmployBlogBridge, EmployBlogBridgeSyncModel>(entryChangeModel);
            }

            if (entryChangeModel.SerializationTargetClass == typeof(Cloud.Persistence.Entities.Presentation).FullName)
            {
                return HandleEntryChange<Cloud.Persistence.Entities.Presentation, PresentationSyncModel>(entryChangeModel);
            }

            if (entryChangeModel.SerializationTargetClass == typeof(Cloud.Persistence.Entities.ProductionTeam).FullName)
            {
                return HandleEntryChange<Cloud.Persistence.Entities.ProductionTeam, ProductionTeamSyncModel>(entryChangeModel);
            }

            if (entryChangeModel.SerializationTargetClass == typeof(Cloud.Persistence.Entities.ProductionTeamEmployBridge).FullName)
            {
                return HandleEntryChange<Cloud.Persistence.Entities.ProductionTeamEmployBridge, ProductionTeamEmployBridgeSyncModel>(entryChangeModel);
            }

            if (entryChangeModel.SerializationTargetClass == typeof(Cloud.Persistence.Entities.Show).FullName)
            {
                return HandleEntryChange<Cloud.Persistence.Entities.Show, ShowSyncModel>(entryChangeModel);
            }

            return false;
        }
    }
}
