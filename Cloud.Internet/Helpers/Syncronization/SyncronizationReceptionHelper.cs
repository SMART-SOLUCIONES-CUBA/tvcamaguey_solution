using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Helpers.Syncronization
{
    public class SyncronizationReceptionHelper : SAPRESSC.Syncronization.Web.Api.Reception.Helpers.Facade.ISyncronizationReceptionHelperFacade
    {
        private readonly Microsoft.EntityFrameworkCore.DbContext dbContext;
        private readonly SAPRESSC.Middleware.Media.Web.Common.Managers.Facade.IImageFileManagerFacade imageFileManagerFacade;
        private readonly SAPRESSC.Middleware.Media.Web.Common.Managers.Facade.IThumbnailFileManagerFacade thumbnailFileManagerFacade;

        public SyncronizationReceptionHelper(SAPRESSC.Middleware.Media.Web.Common.Managers.Facade.IThumbnailFileManagerFacade thumbnailFileManagerFacade, SAPRESSC.Middleware.Media.Web.Common.Managers.Facade.IImageFileManagerFacade imageFileManagerFacade, Microsoft.EntityFrameworkCore.DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.imageFileManagerFacade = imageFileManagerFacade;
            this.thumbnailFileManagerFacade = thumbnailFileManagerFacade;            
        }

        public System.Action<SAPRESSC.Syncronization.Api.Common.Models.SyncronizationOrderModel> PreEntryChangeHandlingAction => null;

        public System.Action<SAPRESSC.Syncronization.Api.Common.Models.SyncronizationOrderModel> PostEntryChangeHandlingAction => PostEntryChangesHandling;

        private void PostEntryChangesHandling(SAPRESSC.Syncronization.Api.Common.Models.SyncronizationOrderModel model)
        {
            var targetClass = typeof(SAPRESSC.Middleware.Media.Persistence.Entities.Image).FullName;

            var targets = model.Changes.Where(m => m.Type == SAPRESSC.Syncronization.Common.Enumerations.EntryChangeType.Delete && m.SerializationTargetClass == targetClass).Select((SAPRESSC.Syncronization.Api.Common.Models.EntryChangeModel m) =>
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<SAPRESSC.Middleware.Media.Syncronization.Handling.Common.Models.ImageSyncModel>(m.Serialization);
            });

            foreach (var target in targets)
            {
                imageFileManagerFacade.DeleteImageFile(new SAPRESSC.Middleware.Media.Persistence.Entities.Image { Id = target.Id, ContentType = target.ContentType });
            }
        }
    }
}
