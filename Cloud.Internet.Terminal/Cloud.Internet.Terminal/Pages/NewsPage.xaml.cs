using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Extensions;
using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
	public partial class NewsPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        private const int PAGING_ITEMS_LIMIT = 10;

        public NewsPage()
		{
			InitializeComponent();

            IsEnabled = false;

            FlowListView.FlowLoadingCommand = new Xamarin.Forms.Command(LoadMoreLocalPosts);

            BindingContext = new Models.Page.NewsPageModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (IsEnabled) return;

            (BindingContext as Models.Page.NewsPageModel).IsBusy = true;

            LoadMoreLocalPosts();

            IsEnabled = true;
        }

        private void UpdateToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            ListView_Refreshing(sender, e);
        }

        private void ListView_Refreshing(object sender, System.EventArgs e)
        {
            (BindingContext as Models.Page.NewsPageModel).IsBusy = true;

            DownloadNextPosts();
        }

        private void LoadMoreLocalPosts()
        {
            var stopwatch = new System.Diagnostics.Stopwatch();

            System.Threading.Tasks.Task.Run(() =>
            {
                stopwatch.Start();

                var posts = new System.Collections.Generic.List<Internet.Persistence.Entities.Post>();

                using (var dbContext = Providers.DbContextProvider.Instance.GetDbContext())
                {
                    if ((BindingContext as Models.Page.NewsPageModel).Total == 0)
                    {
                        (BindingContext as Models.Page.NewsPageModel).Total = dbContext.Posts.Count();
                    }

                    posts = dbContext.Posts.Include(e => e.Poster).OrderByDescending(e => e.PostedAt).Skip((BindingContext as Models.Page.NewsPageModel).Posts.Count).Take(PAGING_ITEMS_LIMIT).ToList();
                }

                return posts;
            }).ContinueWith(async (System.Threading.Tasks.Task<System.Collections.Generic.List<Internet.Persistence.Entities.Post>> task) =>
            {
                stopwatch.Stop();

                var reasonableElapseTime = System.TimeSpan.FromSeconds(2);

                if (stopwatch.Elapsed < reasonableElapseTime)
                {
                    await System.Threading.Tasks.Task.Delay(reasonableElapseTime - stopwatch.Elapsed);
                }

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    (BindingContext as Models.Page.NewsPageModel).Posts.AddRange(task.Result.ToViewModels());

                    (BindingContext as Models.Page.NewsPageModel).IsBusy = false;

                    //(BindingContext as Models.Page.NewsPageModel).IsLoadingInfinite = false;
                });
            });
        }

        private void DownloadNextPosts()
        {
            System.Threading.Tasks.Task.Run(async () =>
            {
                var client = new Api.Connect.Clients.PostsApiClient(Providers.HttpClientHelperProvider.Instance);

                SAPRESSC.Core.Common.Models.PaginationModel<Api.Common.Models.PostDisplayModel> pagination;

                var last = (BindingContext as Models.Page.NewsPageModel).Posts.LastOrDefault();

                if (last == null)
                {
                    pagination = await client.GetPaginateAsync(limit:50);
                }
                else
                {
                    pagination = await client.FilterPaginateAsync(new Api.Common.Models.Filter.PostFilterModel { PostedSameOrAfterAt = last.Post.PostedAt }, limit: 50);
                }

                var remoteEntitiesQueue = new System.Collections.Generic.Queue<Internet.Persistence.Entities.Post>();

                foreach (var model in pagination.List)
                {
                    remoteEntitiesQueue.Enqueue(model.ToPersistenceEntity());
                }

                var posts = new System.Collections.Generic.List<Internet.Persistence.Entities.Post>();

                var deleteImageEntities = new System.Collections.Generic.List<SAPRESSC.Middleware.Media.Persistence.Entities.Image>();

                using (var dbContext = Providers.DbContextProvider.Instance.GetDbContext())
                {
                    var downloadImageEntities = new System.Collections.Generic.List<SAPRESSC.Middleware.Media.Persistence.Entities.Image>();

                    while (remoteEntitiesQueue.Count > 0)
                    {
                        var remotePost = remoteEntitiesQueue.Dequeue();

                        var localPost = dbContext.Posts.Include(e => e.Category).Include(e => e.Poster).FirstOrDefault(e => e.Id == remotePost.Id);

                        dbContext.AddOrUpdate(localPost, remotePost, deleteImageEntities, downloadImageEntities);
                    }

                    dbContext.Images.RemoveRange(deleteImageEntities);

                    foreach (var image in downloadImageEntities)
                    {
                        await Helpers.ImageHandlingHelper.FetchRemoteImageFileAsync(image.Id, image.ContentType);
                    }

                    dbContext.SaveChanges(string.Empty, syncronize: false);

                    (BindingContext as Models.Page.NewsPageModel).Total = dbContext.Posts.Count();

                    posts = dbContext.Posts.OrderByDescending(e => e.PostedAt).Skip(0).Take(PAGING_ITEMS_LIMIT).ToList();
                }

                try
                {
                    foreach (var target in deleteImageEntities)
                    {
                        Helpers.ImageHandlingHelper.RemoveLocalImageFile(target.Id, target.ContentType);
                    }
                }
                catch { }

                return posts;
            }).ContinueWith((System.Threading.Tasks.Task<System.Collections.Generic.List<Internet.Persistence.Entities.Post>> task) =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    if (task.Exception != null || task.IsCanceled)
                    {
                        DisplayAlert("Aviso", "No fue posible actualizar las Noticias de Televisión Camagüey en este momento. Revise la conexión Red Wi-Fi o Datos Móviles de su dispositivo.", "Aceptar");
                    }
                    else
                    {
                        if (task.Result.Count > 0)
                        {
                            (BindingContext as Models.Page.NewsPageModel).Posts.Repopulate(task.Result.ToViewModels());
                        }
                    }

                    (BindingContext as Models.Page.NewsPageModel).IsBusy = false;
                });
            });
        }
    }
}