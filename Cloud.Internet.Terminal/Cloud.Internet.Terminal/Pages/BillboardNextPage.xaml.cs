using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Extensions;
using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class BillboardNextPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        public BillboardNextPage()
        {
            InitializeComponent();

            IsEnabled = false;

            BindingContext = new Models.Page.BillboardNextPageModel
            {
                IsBusy = true
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (IsEnabled) return;

            LoadLocalDayPresentations();

            IsEnabled = true;
        }

        private void UpdateToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            (BindingContext as Models.Page.BillboardNextPageModel).IsBusy = true;

            (BindingContext as Models.Page.BillboardNextPageModel).DayPresentations = null;

            DownloadNextPresentations();
        }

        private void ListView_Refreshing(object sender, System.EventArgs e)
        {
            (BindingContext as Models.Page.BillboardNextPageModel).IsBusy = true;

            (BindingContext as Models.Page.BillboardNextPageModel).DayPresentations = null;

            DownloadNextPresentations();
        }

        private void ListView_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var target = (e.SelectedItem as Models.View.BasePresentationViewModel).Presentation;

            if (target == null)
            {
                (sender as Xamarin.Forms.ListView).SelectedItem = null;

                return;
            }

            this.NavigationPushPageAsync(new PresentationDetailPage(target));
        }

        private void LoadLocalDayPresentations()
        {
            var startAt = System.DateTime.Now;

            var endAt = default(System.DateTime);

            var stopwatch = new System.Diagnostics.Stopwatch();

            System.Threading.Tasks.Task.Run(() =>
            {
                stopwatch.Start();

                var presentations = new System.Collections.Generic.List<Cloud.Persistence.Entities.Presentation>();

                using (var dbContext = Providers.DbContextProvider.Instance.GetDbContext())
                {
                    presentations = dbContext.Presentations.Include(e => e.Show).ThenInclude(e => e.Poster).Where(e => e.At.Date >= startAt).OrderBy(e => e.At).ToList();

                    if (presentations.Count > 0)
                    {
                        endAt = presentations.Last().At.Date;
                    }
                    else
                    {
                        endAt = startAt.AddDays(7);
                    }
                }

                return presentations;
            }).ContinueWith(async (System.Threading.Tasks.Task<System.Collections.Generic.List<Cloud.Persistence.Entities.Presentation>> task) =>
            {
                stopwatch.Stop();

                var reasonableElapseTime = System.TimeSpan.FromSeconds(1);

                if (stopwatch.Elapsed < reasonableElapseTime)
                {
                    await System.Threading.Tasks.Task.Delay(reasonableElapseTime - stopwatch.Elapsed);
                }

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    if ((BindingContext as Models.Page.BillboardNextPageModel).DayPresentations == null)
                    {
                        (BindingContext as Models.Page.BillboardNextPageModel).DayPresentations = new System.Collections.ObjectModel.ObservableCollection<Models.View.DayPresentationsViewModel>();
                    }

                    if (task.Result.Count > 0)
                    {
                        var dayPresentationsStack = new System.Collections.Generic.Stack<Models.View.DayPresentationsViewModel>(task.Result.ToDayPresentationsViewModels(startAt.Date, endAt.Date));

                        while (dayPresentationsStack.Count > 0)
                        {
                            var dayPresentation = dayPresentationsStack.Pop();

                            (BindingContext as Models.Page.BillboardNextPageModel).DayPresentations.Insert(0, dayPresentation);
                        }
                    }

                    (BindingContext as Models.Page.BillboardNextPageModel).IsBusy = false;
                });
            });
        }

        private void DownloadNextPresentations()
        {
            var filter = new Api.Common.Models.Filter.PresentationFilterModel
            {
                StartAt = System.DateTime.Now.Date
            };

            System.Threading.Tasks.Task.Run(async () =>
            {
                var client = new Api.Connect.Clients.PresentationsApiClient(Providers.HttpClientHelperProvider.Instance);

                var models = await client.FilterAsync(filter);

                var remoteEntitiesQueue = new System.Collections.Generic.Queue<Cloud.Persistence.Entities.Presentation>();

                foreach (var model in models)
                {
                    remoteEntitiesQueue.Enqueue(model.ToPersistenceEntity());
                }

                var presentations = new System.Collections.Generic.List<Cloud.Persistence.Entities.Presentation>();

                var deleteImageEntities = new System.Collections.Generic.List<SAPRESSC.Middleware.Media.Persistence.Entities.Image>();

                using (var dbContext = Providers.DbContextProvider.Instance.GetDbContext())
                {
                    var downloadImageEntities = new System.Collections.Generic.List<SAPRESSC.Middleware.Media.Persistence.Entities.Image>();

                    while (remoteEntitiesQueue.Count > 0)
                    {
                        var remotePresentation = remoteEntitiesQueue.Dequeue();

                        var localPresentation = dbContext.Presentations.FirstOrDefault(e => e.Id == remotePresentation.Id);

                        dbContext.AddOrUpdate(localPresentation, remotePresentation, deleteImageEntities, downloadImageEntities);
                    }

                    dbContext.Images.RemoveRange(deleteImageEntities);

                    foreach (var image in downloadImageEntities)
                    {
                        await Helpers.ImageHandlingHelper.FetchRemoteImageFileAsync(image.Id, image.ContentType);
                    }

                    dbContext.SaveChanges(string.Empty, syncronize: false);

                    presentations = dbContext.Presentations.Include(e => e.Show).ThenInclude(e => e.Poster).Where(e => e.At.Date >= System.DateTime.Now.Date).OrderBy(e => e.At).ToList();
                }

                try
                {
                    foreach (var target in deleteImageEntities)
                    {
                        Helpers.ImageHandlingHelper.RemoveLocalImageFile(target.Id, target.ContentType);
                    }
                }
                catch { }

                return presentations;
            }).ContinueWith((System.Threading.Tasks.Task<System.Collections.Generic.List<Cloud.Persistence.Entities.Presentation>> task) =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    if (task.Exception != null || task.IsCanceled)
                    {
                        DisplayAlert("Aviso", "No fue posible actualizar la Cartelera de Televisión Camagüey en este momento. Revise la conexión Red Wi-Fi o Datos Móviles de su dispositivo.", "Aceptar");
                    }

                    LoadLocalDayPresentations();
                });
            });
        }        
    }
}
