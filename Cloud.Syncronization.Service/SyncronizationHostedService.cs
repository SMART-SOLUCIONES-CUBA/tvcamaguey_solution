using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Api.Connect.Helpers;
using SmartSolucionesCuba.TVCamaguey.Cloud.Common.Extensions;
using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Syncronization.Service
{
    public class SyncronizationHostedService : Microsoft.Extensions.Hosting.BackgroundService
    {
        private readonly ILogger<SyncronizationHostedService> logger;
        private readonly Microsoft.Extensions.Options.IOptions<Options.SyncronizationHandlingOptions> handlingOptions;

        public SyncronizationHostedService(Microsoft.Extensions.Options.IOptions<Options.SyncronizationHandlingOptions> handlingOptions, ILogger<SyncronizationHostedService> logger)
        {
            this.logger = logger;
            this.handlingOptions = handlingOptions;
        } 

        protected override async System.Threading.Tasks.Task ExecuteAsync(System.Threading.CancellationToken stoppingToken)
        {
            await System.Threading.Tasks.Task.Delay(handlingOptions.Value.IterationDesiredElapseTime);

            while (!stoppingToken.IsCancellationRequested)
            {
                #region Start Iteration Trace

                var startedAt = System.DateTime.Now;

                logger.LogDebug("Request handling iteration started at {0}", startedAt);

                #endregion
                try
                {
                    var intranetSyncronizationOrdersDeliveryApiClient = new SAPRESSC.Syncronization.Api.Connect.Delivery.Clients.SyncronizationOrdersDeliveryApiClient(new IntranetCloudHttpClientHelperProvider(handlingOptions));

                    var internetCloudHttpClientHelperProvider = new InternetCloudHttpClientHelperProvider(handlingOptions);

                    var internetSyncronizationOrdersReceptionApiClient = new SAPRESSC.Syncronization.Api.Connect.Reception.Clients.SyncronizationOrdersReceptionApiClient(internetCloudHttpClientHelperProvider);
                    var internetMediaFileUploadClient = new Internet.Api.Connect.Clients.MediFileUploadApiClient(internetCloudHttpClientHelperProvider);

                    var iterationPendingSyncronizationOrdersPaginationModel = await intranetSyncronizationOrdersDeliveryApiClient.GetPaginateAsync(1, handlingOptions.Value.IterationRequestCount);

                    foreach (var syncronizationOrder in iterationPendingSyncronizationOrdersPaginationModel.List)
                    {
                        var mediaImageDbEntryChanges = syncronizationOrder.Changes.Where(x => x.Type == SAPRESSC.Syncronization.Common.Enumerations.EntryChangeType.Add && x.SerializationTargetClass == typeof(SAPRESSC.Middleware.Media.Persistence.Entities.Image).FullName).ToArray();

                        foreach (var mediaImageDbEntryChange in mediaImageDbEntryChanges)
                        {
                            var imageSyncModel = Newtonsoft.Json.JsonConvert.DeserializeObject<SAPRESSC.Middleware.Media.Syncronization.Handling.Common.Models.ImageSyncModel>(mediaImageDbEntryChange.Serialization);

                            var imageRequest = System.Net.WebRequest.Create(string.Format("http://{0}/media/images/{1}.{2}", handlingOptions.Value.IntranetCloudServer, imageSyncModel.Id, imageSyncModel.ContentType.ContentTypeToExtension()));

                            imageRequest.Method = System.Net.WebRequestMethods.Http.Get;

                            var imageResponse = imageRequest.GetResponse();

                            using (var memoryStream = new System.IO.MemoryStream())
                            {
                                using (var imageResponseStream = imageResponse.GetResponseStream())
                                {
                                    await imageResponseStream.CopyToAsync(memoryStream);
                                }

                                memoryStream.Position = 0;

                                var mediaFileUploadModel = new Internet.Api.Common.Models.MediaFileUploadModel
                                {
                                    Id = imageSyncModel.Id,
                                    ContentType = imageSyncModel.ContentType,
                                    ContentData = memoryStream.ToArray()
                                };

                                await internetMediaFileUploadClient.UploadImageAsync(mediaFileUploadModel);
                            }
                        }

                        logger.LogDebug("Pushing Syncronization Order");

                        await internetSyncronizationOrdersReceptionApiClient.ReceptAsync(syncronizationOrder);

                        logger.LogDebug("Removing Syncronization Order");

                        await intranetSyncronizationOrdersDeliveryApiClient.Delete(syncronizationOrder.Id);
                    }
                }
                catch (SAPRESSC.Core.Api.Connect.UnspectedHttpStatusException ex)
                {
                    logger.LogError(ex, "Cloud Api Syncronization Handling Error! ({0})", ex.HttpStatusCode);
                }
                catch (System.Exception ex)
                {
                    logger.LogError(ex, "Unexpected Syncronization Handling Error!");
                }

                #region End Iteration Trace

                var endedAt = System.DateTime.Now;

                var elpaseTime = endedAt - startedAt;

                logger.LogDebug("Request handling iteration ended at {0}", endedAt);

                var delayTime = handlingOptions.Value.IterationDesiredElapseTime - elpaseTime;

                if (delayTime < System.TimeSpan.Zero) continue;

                logger.LogDebug("Request handling iteration weiting {0}", delayTime);

                await System.Threading.Tasks.Task.Delay(delayTime);

                #endregion
            }
        }

        private sealed class IntranetCloudHttpClientHelperProvider : SAPRESSC.Core.Api.Connect.Providers.Facade.IHttpClientHelperProviderFacade
        {
            private readonly Microsoft.Extensions.Options.IOptions<Options.SyncronizationHandlingOptions> handlingOptions;

            public IntranetCloudHttpClientHelperProvider(Microsoft.Extensions.Options.IOptions<Options.SyncronizationHandlingOptions> handlingOptions)
            {
                this.handlingOptions = handlingOptions;
            }

            public HttpClientHelper HttpClientHelper => new HttpClientHelper(string.Format("http://{0}/api/", handlingOptions.Value.IntranetCloudServer), null, null);
        }

        private sealed class InternetCloudHttpClientHelperProvider : SAPRESSC.Core.Api.Connect.Providers.Facade.IHttpClientHelperProviderFacade
        {
            private readonly Microsoft.Extensions.Options.IOptions<Options.SyncronizationHandlingOptions> handlingOptions;

            public InternetCloudHttpClientHelperProvider(Microsoft.Extensions.Options.IOptions<Options.SyncronizationHandlingOptions> handlingOptions)
            {
                this.handlingOptions = handlingOptions;
            }

            public HttpClientHelper HttpClientHelper => new HttpClientHelper(string.Format("http://{0}/api/", handlingOptions.Value.InternetCloudServer), null, null);
        }
    }
}   
