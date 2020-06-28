using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Extensions;
using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
	public partial class ShowsPage : XFToolKit.Mvvm.Pages.ContentPage
    {
		public ShowsPage()
		{
			InitializeComponent();

            IsEnabled = false;

            BindingContext = new Models.Page.ShowsPageModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (IsEnabled) return;

            (BindingContext as Models.Page.ShowsPageModel).IsBusy = true;

            LoadShows();

            IsEnabled = true;
        }

        private void ListView_Refreshing(object sender, System.EventArgs e)
        {
            (BindingContext as Models.Page.ShowsPageModel).IsBusy = true;

            DownloadShows();
        }

        private void UpdateToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            if ((BindingContext as Models.Page.ShowsPageModel).IsBusy) return;

            (BindingContext as Models.Page.ShowsPageModel).IsBusy = true;

            DownloadShows();
        }

        private void LoadShows()
        {
            var stopwatch = new System.Diagnostics.Stopwatch();

            System.Threading.Tasks.Task.Run(() =>
            {
                stopwatch.Start();

                var shows = new System.Collections.Generic.List<Cloud.Persistence.Entities.Show>();

                using (var dbContext = Providers.DbContextProvider.Instance.GetDbContext())
                {
                    shows = dbContext.Shows.Include(e => e.Poster).ToList();
                }

                return shows;
            }).ContinueWith(async (System.Threading.Tasks.Task<System.Collections.Generic.List<Cloud.Persistence.Entities.Show>> task) =>
            {
                stopwatch.Stop();

                var reasonableElapseTime = System.TimeSpan.FromSeconds(1);

                if (stopwatch.Elapsed < reasonableElapseTime)
                {
                    await System.Threading.Tasks.Task.Delay(reasonableElapseTime - stopwatch.Elapsed);
                }

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    (BindingContext as Models.Page.ShowsPageModel).Shows.Repopulate(task.Result);

                    (BindingContext as Models.Page.ShowsPageModel).IsBusy = false;
                });
            });
        }

        private void DownloadShows()
        {
            System.Threading.Tasks.Task.Run(async () =>
            {
                var client = new Api.Connect.Clients.ShowsApiClient(Providers.HttpClientHelperProvider.Instance);

                var models = await client.GetAsync();

                var remoteEntitiesQueue = new System.Collections.Generic.Queue<Cloud.Persistence.Entities.Show>();

                foreach (var model in models)
                {
                    remoteEntitiesQueue.Enqueue(model.ToPersistenceEntity());
                }

                var shows = new System.Collections.Generic.List<Cloud.Persistence.Entities.Show>();

                var deleteImageEntities = new System.Collections.Generic.List<SAPRESSC.Middleware.Media.Persistence.Entities.Image>();

                using (var dbContext = Providers.DbContextProvider.Instance.GetDbContext())
                {
                    var downloadImageEntities = new System.Collections.Generic.List<SAPRESSC.Middleware.Media.Persistence.Entities.Image>();

                    var localEntitiesList = dbContext.Shows.ToList();

                    while (remoteEntitiesQueue.Count > 0)
                    {
                        var remoteShow = remoteEntitiesQueue.Dequeue();

                        var localShow = localEntitiesList.FirstOrDefault(e => e.Id == remoteShow.Id);

                        dbContext.AddOrUpdate(localShow, remoteShow, deleteImageEntities, downloadImageEntities, localEntitiesList);
                    }

                    foreach (var localEntity in localEntitiesList)
                    {
                        deleteImageEntities.Add(localEntity.Logo);
                        deleteImageEntities.Add(localEntity.Poster);

                        dbContext.Shows.Remove(localEntity);
                    }

                    dbContext.Images.RemoveRange(deleteImageEntities);

                    foreach (var image in downloadImageEntities)
                    {
                        await Helpers.ImageHandlingHelper.FetchRemoteImageFileAsync(image.Id, image.ContentType);
                    }

                    dbContext.SaveChanges(string.Empty, syncronize: false);

                    shows = dbContext.Shows.Include(e => e.Poster).OrderBy(e => e.Name).ToList();
                }

                try
                {
                    foreach (var target in deleteImageEntities)
                    {
                        Helpers.ImageHandlingHelper.RemoveLocalImageFile(target.Id, target.ContentType);
                    }
                }
                catch { }

                return shows;
            }).ContinueWith((System.Threading.Tasks.Task<System.Collections.Generic.List<Cloud.Persistence.Entities.Show>> task) =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    if (task.Exception != null || task.IsCanceled)
                    {
                        DisplayAlert("Aviso", "No fue posible actualizar los Programas de Televisión Camagüey en este momento. Revise la conexión Red Wi-Fi o Datos Móviles de su dispositivo.", "Aceptar"); 
                    }
                    else
                    {
                        (BindingContext as Models.Page.ShowsPageModel).Shows.Repopulate(task.Result);
                    }

                    (BindingContext as Models.Page.ShowsPageModel).IsBusy = false;
                });
            });
        }
    }
}