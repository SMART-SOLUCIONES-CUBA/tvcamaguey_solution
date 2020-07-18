using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Extensions;
using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;
using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
	public partial class HomePage : XFToolKit.Mvvm.Pages.ContentPage
    {
		public HomePage()
		{
            InitializeComponent();

            IsEnabled = false;

            BindingContext = new Models.Page.HomePageModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (IsEnabled) return;

            LoadSections();
        }

        private void ViewMoreBillboardButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            ((Xamarin.Forms.Application.Current.MainPage as MainPage).Master as MasterPage).PresentBillboardSection();
        }

        private void ViewMoreNewsButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            ((Xamarin.Forms.Application.Current.MainPage as MainPage).Master as MasterPage).PresentNewsSection();
        }

        private void PostsItem_Tapped(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPushPageAsync(new PostDetailPage(((sender as Xamarin.Forms.BindableObject).BindingContext as Models.View.BasePostViewModel).Post));
        }

        private void PresentationsItem_Tapped(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPushPageAsync(new PresentationDetailPage((sender as Xamarin.Forms.BindableObject).BindingContext as Cloud.Persistence.Entities.Presentation));
        }

        private void UpdateToolbarItem_Clicked(object sender, System.EventArgs args)
        {
            (BindingContext as Models.Page.HomePageModel).IsBusy = true;
        }

        private void RefreshView_Refreshing(object sender, System.EventArgs args)
        {
            DownloadCloudData();
        }

        private void DownloadCloudData()
        {
            var presentationFilter = new Api.Common.Models.Filter.PresentationFilterModel
            {
                StartAt = System.DateTime.Now.Date
            };

            System.Threading.Tasks.Task.Run(async () =>
            {
                var presentationClient = new Api.Connect.Clients.PresentationsApiClient(Providers.HttpClientHelperProvider.Instance);
                var showClient = new Api.Connect.Clients.ShowsApiClient(Providers.HttpClientHelperProvider.Instance);
                var postClient = new Api.Connect.Clients.PostsApiClient(Providers.HttpClientHelperProvider.Instance);


                var presentationModels = await presentationClient.FilterAsync(presentationFilter);
                var showModels = await showClient.GetAsync();
                var postPagination = await postClient.GetPaginateAsync(limit: 50);

                var remotePresentationEntitiesQueue = new System.Collections.Generic.Queue<Cloud.Persistence.Entities.Presentation>();
                var remoteShowEntitiesQueue = new System.Collections.Generic.Queue<Cloud.Persistence.Entities.Show>();
                var remotePostEntitiesQueue = new System.Collections.Generic.Queue<Internet.Persistence.Entities.Post>();

                foreach (var model in presentationModels)
                {
                    remotePresentationEntitiesQueue.Enqueue(model.ToPersistenceEntity());
                }

                foreach (var model in showModels)
                {
                    remoteShowEntitiesQueue.Enqueue(model.ToPersistenceEntity());
                }

                foreach (var model in postPagination.List)
                {
                    remotePostEntitiesQueue.Enqueue(model.ToPersistenceEntity());
                }

                var deleteImageEntities = new System.Collections.Generic.List<SAPRESSC.Middleware.Media.Persistence.Entities.Image>();

                using (var dbContext = Providers.DbContextProvider.Instance.GetDbContext())
                {
                    var downloadImageEntities = new System.Collections.Generic.List<SAPRESSC.Middleware.Media.Persistence.Entities.Image>();

                    var localShowEntitiesList = dbContext.Shows.ToList();

                    while (remoteShowEntitiesQueue.Count > 0)
                    {
                        var remoteShow = remoteShowEntitiesQueue.Dequeue();

                        var localShow = localShowEntitiesList.FirstOrDefault(e => e.Id == remoteShow.Id);

                        dbContext.AddOrUpdate(localShow, remoteShow, deleteImageEntities, downloadImageEntities, localShowEntitiesList);
                    }

                    foreach (var localEntity in localShowEntitiesList)
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
                }

                using (var dbContext = Providers.DbContextProvider.Instance.GetDbContext())
                {
                    var downloadImageEntities = new System.Collections.Generic.List<SAPRESSC.Middleware.Media.Persistence.Entities.Image>();

                    while (remotePresentationEntitiesQueue.Count > 0)
                    {
                        var remotePresentation = remotePresentationEntitiesQueue.Dequeue();

                        var localPresentation = dbContext.Presentations.FirstOrDefault(e => e.Id == remotePresentation.Id);

                        dbContext.AddOrUpdate(localPresentation, remotePresentation, deleteImageEntities, downloadImageEntities);
                    }

                    while (remotePostEntitiesQueue.Count > 0)
                    {
                        var remotePost = remotePostEntitiesQueue.Dequeue();

                        var localPost = dbContext.Posts.Include(e => e.Category).Include(e => e.Poster).Include(e => e.TagBridges).ThenInclude(b => b.Tag).FirstOrDefault(e => e.Id == remotePost.Id);

                        dbContext.AddOrUpdate(localPost, remotePost, deleteImageEntities, downloadImageEntities);
                    }

                    dbContext.Images.RemoveRange(deleteImageEntities);

                    foreach (var image in downloadImageEntities)
                    {
                        await Helpers.ImageHandlingHelper.FetchRemoteImageFileAsync(image.Id, image.ContentType);
                    }

                    dbContext.SaveChanges(string.Empty, syncronize: false);
                }

                try
                {
                    foreach (var target in deleteImageEntities)
                    {
                        Helpers.ImageHandlingHelper.RemoveLocalImageFile(target.Id, target.ContentType);
                    }
                }
                catch { }
            }).ContinueWith(async task =>
            {
                if (task.Exception != null)
                {
                    await Managers.SessionManager.Instance.SaveUpdatedDateTimeAsync();
                }

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    if (task.Exception != null || task.IsCanceled)
                    {
                        DisplayAlert("Aviso", "No fue posible actualizar la Cartelera de Televisión Camagüey en este momento. Revise la conexión Red Wi-Fi o Datos Móviles de su dispositivo.", "Aceptar");
                    }

                    (BindingContext as Models.Page.HomePageModel).IsBusy = false;

                    (BindingContext as Models.Page.HomePageModel).Presentations = null;
                    (BindingContext as Models.Page.HomePageModel).Posts = null;

                    LoadSections();
                });
            });
        }

        private void LoadSections()
        {
            var stopwatch = new System.Diagnostics.Stopwatch();

            System.Threading.Tasks.Task.Run(async () =>
            {
                stopwatch.Start();

                var posts = new System.Collections.Generic.List<Internet.Persistence.Entities.Post>();
                var presentations = new System.Collections.Generic.List<Cloud.Persistence.Entities.Presentation>();

                using (var dbContext = Providers.DbContextProvider.Instance.GetDbContext())
                {
                    posts = dbContext.Posts.Include(e => e.Poster).OrderByDescending(e => e.PostedAt).Include(e => e.Category).Take(10).ToList();
                    presentations = dbContext.Presentations.Include(e => e.Show).ThenInclude(e => e.Poster).Where(e => e.At >= System.DateTime.Now).OrderBy(e => e.At).Take(10).ToList();
                }

                stopwatch.Stop();

                var reasonableElapseTime = System.TimeSpan.FromSeconds(2);

                if (stopwatch.Elapsed < reasonableElapseTime)
                {
                    await System.Threading.Tasks.Task.Delay(reasonableElapseTime - stopwatch.Elapsed);
                }

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    (BindingContext as Models.Page.HomePageModel).Posts = posts.Count > 0 ? posts.ToViewModels() : new System.Collections.Generic.List<Models.View.BasePostViewModel>();

                    (BindingContext as Models.Page.HomePageModel).Presentations = presentations.Count > 0 ? presentations : new System.Collections.Generic.List<Cloud.Persistence.Entities.Presentation>();

                    IsEnabled = true;

                    if (Managers.SessionManager.Instance.LastUpdateDateTime != default) return;

                    if (Managers.SessionManager.Instance.IsUpdateRequested) return;

                    DisplayAlert("Aviso", string.Format("La aplicación en su primer uso no cuenta con información de las actividades de Televisión Camagüey.{0}{0}Desea actualizar estos datos ahora?", System.Environment.NewLine), "Actualizar", "Después").ContinueWith(async task =>
                    {
                        await Managers.SessionManager.Instance.SaveUpdateRequestedAsync();

                        if (!task.Result) return;

                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            (BindingContext as Models.Page.HomePageModel).IsBusy = true;
                        });
                    });
                });
            });
        }
    }
}