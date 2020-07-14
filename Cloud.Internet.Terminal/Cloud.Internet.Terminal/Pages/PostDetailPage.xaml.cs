using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Extensions;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Pages
{
	[Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
	public partial class PostDetailPage : XFToolKit.Mvvm.Pages.ContentPage
    {
		public PostDetailPage(Internet.Persistence.Entities.Post post)
		{
			InitializeComponent();

			IsEnabled = false;

			BindingContext = new Models.Page.PostDetailPageModel
			{
				IsBusy = true,
                Id = post.Id,
                AuthorId = post.AuthorId,
                Title = post.Title,
                Summary = post.Summary,
                PostedAt = post.PostedAt,
                Category = post.Category,
                Content = Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android ? post.Content : post.Content.ToStandarizedHtmlContent(),
                Poster = post.Poster
			};
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (IsEnabled) return;

            IsEnabled = true;

            var pageBindingContext = BindingContext as Models.Page.PostDetailPageModel;

            System.Threading.Tasks.Task.Run(() =>
            {
                System.Collections.Generic.List<Internet.Persistence.Entities.Tag> tags = null;
                Cloud.Persistence.Entities.Employ author = null;

                using (var dbContext = Providers.DbContextProvider.Instance.GetDbContext())
                {
                    author = dbContext.Employs.Include(e => e.Identity).ThenInclude(e => e.Image).First(e => e.Id == pageBindingContext.AuthorId);

                    tags = dbContext.PostTagBridges.Include(b => b.Tag).Where(b => b.PostId == pageBindingContext.Id).Select(b => b.Tag).ToList();
                }

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    pageBindingContext.Author = author;

                    if (tags.Count > 0)
                    {
                        pageBindingContext.Tags = tags;
                    }

                    pageBindingContext.IsBusy = false;
                });
            });
        }
    }
}