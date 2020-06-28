using SmartSolucionesCuba.SAPRESSC.Syncronization.Persistence.Entities;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Helpers.Syncronization
{
    public class SyncronizationDeliveryHelper : SAPRESSC.Syncronization.Web.Api.Delivery.Helpers.Facade.ISyncronizationDeliveryHelperFacade
    {
        public System.Action<SyncronizationOrder> PreSyncronizationOrderDeleteAction => null;

        public System.Action<SyncronizationOrder> PostSyncronizationOrderDeleteAction => null;
    }
}
