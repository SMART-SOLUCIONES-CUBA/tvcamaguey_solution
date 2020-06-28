using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Extensions;
using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Pages
{
	[Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
	public partial class BillboardPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        private System.DateTime startAt;
        private System.DateTime endAt;

		public BillboardPage()
		{
			InitializeComponent();

            IsEnabled = false;

            BindingContext = new Models.Page.BillboardPageModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (IsEnabled) return;

            ListView_Refreshing(this, System.EventArgs.Empty);

            IsEnabled = true;
        }
        
        private void BeforesToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            ListView_Refreshing(this, System.EventArgs.Empty);
        }

        private void UpdateToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            (BindingContext as Models.Page.BillboardPageModel).IsBusy = true;

            DownloadNextPresentations();
        }

        private void ListView_Refreshing(object sender, System.EventArgs e)
        {
            (BindingContext as Models.Page.BillboardPageModel).IsBusy = true;

            LoadLocalDayPresentations();
        }

        private void LoadLocalDayPresentations(bool allowRemote = false)
        {
            var endAt = default(System.DateTime);

            var stopwatch = new System.Diagnostics.Stopwatch();

            System.Threading.Tasks.Task.Run(() =>
            {
                stopwatch.Start();

                if (startAt == default(System.DateTime))
                {
                    startAt = System.DateTime.Now.Date;
                }

                var presentations = new System.Collections.Generic.List<Cloud.Persistence.Entities.Presentation>();

                using (var dbContext = Providers.DbContextProvider.Instance.GetDbContext())
                {
                    if (this.endAt == default(System.DateTime))
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
                    else
                    {
                        endAt = startAt.AddDays(-1);

                        startAt = startAt.AddDays(-7);

                        presentations = dbContext.Presentations.Include(e => e.Show).ThenInclude(e => e.Poster).Where(e => e.At.Date >= startAt && e.At.Date <= endAt).OrderBy(e => e.At).ToList();
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
                    var dayPresentationsStack = new System.Collections.Generic.Stack<Models.View.DayPresentationsViewModel>(task.Result.ToDayPresentationsViewModels(startAt, endAt));

                    while (dayPresentationsStack.Count > 0)
                    {
                        var dayPresentation = dayPresentationsStack.Pop();

                        (BindingContext as Models.Page.BillboardPageModel).DayPresentations.Insert(0, dayPresentation);

                        if (dayPresentation.Date < startAt)
                        {
                            startAt = dayPresentation.Date.Date;
                        }

                        if (dayPresentation.Date > this.endAt)
                        {
                            this.endAt = dayPresentation.Date.Date;
                        }
                    }

                    (BindingContext as Models.Page.BillboardPageModel).IsBusy = false;
                });
            });
        }

        private void DownloadNextPresentations()
        {
            var filter = new Api.Common.Models.Filter.PresentationFilterModel
            {
                StartAt = System.DateTime.Now.Date
            };

            FetchPresentationsAsync(filter).ContinueWith((System.Threading.Tasks.Task<System.Collections.Generic.List<Cloud.Persistence.Entities.Presentation>> task) =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    if (task.Exception != null || task.IsCanceled)
                    {
                        DisplayAlert("Aviso", "No fue posible actualizar la Cartelera de Televisión Camagüey en este momento. Revise la conexión Red Wi-Fi o Datos Móviles de su dispositivo.", "Aceptar");
                    }
                    else
                    {
                        var dayPresentations = task.Result.ToDayPresentationsViewModels(startAt, endAt);

                        if (dayPresentations.Count > 0)
                        {
                            (BindingContext as Models.Page.BillboardPageModel).DayPresentations.Clear();
                        }

                        foreach (var dayPresentation in dayPresentations)
                        {
                            (BindingContext as Models.Page.BillboardPageModel).DayPresentations.Add(dayPresentation);

                            if (dayPresentation.Date < startAt)
                            {
                                startAt = dayPresentation.Date.Date;
                            }

                            if (dayPresentation.Date > endAt)
                            {
                                endAt = dayPresentation.Date.Date;
                            }
                        }                        
                    }

                    (BindingContext as Models.Page.BillboardPageModel).IsBusy = false;
                });
            });
        }

        private async System.Threading.Tasks.Task<System.Collections.Generic.List<Cloud.Persistence.Entities.Presentation>> FetchPresentationsAsync(Api.Common.Models.Filter.PresentationFilterModel filter)
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
        }
    }
}