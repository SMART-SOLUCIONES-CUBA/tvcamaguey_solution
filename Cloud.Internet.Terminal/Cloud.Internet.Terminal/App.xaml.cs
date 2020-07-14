using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.TVCamaguey.Cloud.Common.Extensions;
using System.Linq;

[assembly: Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal
{
    public partial class App : Xamarin.Forms.Application
    {
        private static System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

        public App()
        {
            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("es-US");

            InitializeComponent();

            DLToolkit.Forms.Controls.FlowListView.Init();

            MainPage = new Pages.MainPage();
        }

        protected override void OnStart()
        {
            Xamarin.Forms.Device.StartTimer(System.TimeSpan.FromSeconds(1), SyncronizationTimer__Callback);

            Connectivity_ConnectivityChanged(this, new Xamarin.Essentials.ConnectivityChangedEventArgs(Xamarin.Essentials.Connectivity.NetworkAccess, Xamarin.Essentials.Connectivity.ConnectionProfiles));

            Xamarin.Essentials.Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private void Connectivity_ConnectivityChanged(object sender, Xamarin.Essentials.ConnectivityChangedEventArgs args)
        {
            if (args.NetworkAccess == Xamarin.Essentials.NetworkAccess.None)
            {
                if (stopwatch.IsRunning) stopwatch.Stop();
            }
            else
            {
                if (!stopwatch.IsRunning)
                {
                    stopwatch.Restart();
                }
            }
        }

        protected override void OnSleep()
        {
            stopwatch.Stop();

            Xamarin.Essentials.Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

        protected override void OnResume()
        {
            Connectivity_ConnectivityChanged(this, new Xamarin.Essentials.ConnectivityChangedEventArgs(Xamarin.Essentials.Connectivity.NetworkAccess, Xamarin.Essentials.Connectivity.ConnectionProfiles));

            Xamarin.Essentials.Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        public void HandleException(System.Exception exception)
        {
            System.Exception targetException = exception;

            while (targetException is System.AggregateException)
            {
                targetException = targetException.InnerException;
            }

            System.Action<System.Threading.Tasks.Task> alertContinueAction = (System.Threading.Tasks.Task task) => { };

            var message = "Problemas no esperados en el sistema.";

            targetException = Xamarin.Forms.DependencyService.Get<Helpers.Facade.IExceptionHandlingHelper>().ToCrossPlatformException(targetException);

            if (targetException is System.Net.Http.HttpRequestException || targetException is System.Net.WebException)
            {
                message = "No se puede acceder al servidor. Revise su conexión de red y/o la disponibilidad del servicio.";
            }
            else if (targetException is SAPRESSC.Core.Api.Connect.UnspectedHttpStatusException)
            {
                var unspectedHttpStatus = (targetException as SAPRESSC.Core.Api.Connect.UnspectedHttpStatusException).HttpStatusCode;

                if (unspectedHttpStatus == System.Net.HttpStatusCode.BadRequest)
                {
                    message = "Datos del envío incorrectos.";
                }
                else if (unspectedHttpStatus == System.Net.HttpStatusCode.Unauthorized)
                {
                    message = "Acceso denegado.";

                    alertContinueAction = (System.Threading.Tasks.Task task) =>
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Managers.SessionManager.Instance.CloseAsync();

                            // TODO Navigate to MainPage
                        });
                    };
                }
            }

            MainPage.DisplayAlert("Error", message, "Aceptar").ContinueWith(alertContinueAction);
        }

        private bool SyncronizationTimer__Callback()
        {
            if (stopwatch.IsRunning && stopwatch.Elapsed.Seconds >= 15)
            {
                stopwatch.Stop();

                System.Threading.Tasks.Task.Run(async () =>
                {
                    if (!Managers.SessionManager.Instance.IsAuthenticated)
                    {
                        return;
                    }

                    var syncReceptionApiClient = new SAPRESSC.Syncronization.Api.Connect.Reception.Clients.SyncronizationOrdersReceptionApiClient(Providers.HttpClientHelperProvider.Instance);
                    var mediaFileUploadClient = new Api.Connect.Clients.MediFileUploadApiClient(Providers.HttpClientHelperProvider.Instance);

                    using (var dbContext = Providers.DbContextProvider.Instance.GetDbContext())
                    {
                        var entities = dbContext.SyncronizationOrders.Include(e => e.Changes).ToArray();

                        System.Diagnostics.Debug.WriteLine("########======>>>>>> SyncronizationOrders.Count: {0}", entities.Length);

                        if (entities.Length > 0)
                        {
                            foreach (var entity in entities)
                            {
                                var mediaImageDbEntryChanges = entity.Changes.Where(x => x.Type == SAPRESSC.Syncronization.Common.Enumerations.EntryChangeType.Add && x.SerializationTargetClass == typeof(SAPRESSC.Middleware.Media.Persistence.Entities.Image).FullName).ToArray();

                                foreach (var mediaImageDbEntryChange in mediaImageDbEntryChanges)
                                {
                                    var imageSyncModel = Newtonsoft.Json.JsonConvert.DeserializeObject<SAPRESSC.Middleware.Media.Syncronization.Handling.Common.Models.ImageSyncModel>(mediaImageDbEntryChange.Serialization);

                                    using (var memoryStream = new System.IO.MemoryStream())
                                    {
                                        var imageFilePath = Helpers.LocalFileStorageHelper.GetImageFilePath(imageSyncModel.Id, imageSyncModel.ContentType.ContentTypeToExtension());

                                        using (var imageFileStream = new System.IO.FileStream(imageFilePath, System.IO.FileMode.Open))
                                        {
                                            imageFileStream.CopyTo(memoryStream);
                                        }

                                        memoryStream.Position = 0;

                                        var mediaFileUploadModel = new Api.Common.Models.MediaFileUploadModel
                                        {
                                            Id = imageSyncModel.Id,
                                            ContentType = imageSyncModel.ContentType,
                                            ContentData = memoryStream.ToArray()
                                        };

                                        await mediaFileUploadClient.UploadImageAsync(mediaFileUploadModel);
                                    }
                                }

                                var model = new SAPRESSC.Syncronization.Api.Common.Models.SyncronizationOrderModel
                                {
                                    Id = entity.Id,
                                    At = entity.At,
                                    Tag = entity.Tag,
                                    Changes = entity.Changes.Select(e => new SAPRESSC.Syncronization.Api.Common.Models.EntryChangeModel
                                    {
                                        Id = e.Id,
                                        Type = e.Type,
                                        SerializationTargetClass = e.SerializationTargetClass,
                                        Serialization = e.Serialization
                                    }).ToList()
                                };

                                await syncReceptionApiClient.ReceptAsync(model);

                                dbContext.SyncronizationOrders.Remove(entity);

                                await dbContext.SaveChangesAsync(string.Empty, false, true);
                            }
                        }
                    }
                }).ContinueWith((System.Threading.Tasks.Task task) =>
                {
                    stopwatch.Restart();
                });
            }

            return true;
        }
    }
}
