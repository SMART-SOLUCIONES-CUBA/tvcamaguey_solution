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
            // TODO Uncomment xaml code block when Xamarin.Form release a version with MediaELement fixed!
			InitializeComponent();

            IsEnabled = false;

            BindingContext = new Models.Page.HomePageModel
            {
                IsBusy = true
            };
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
                    presentations = dbContext.Presentations.Include(e => e.Show).ThenInclude(e => e.Poster).Where(e => e.At.Date >= System.DateTime.Now).OrderBy(e => e.At).Take(10).ToList();
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

                    (BindingContext as Models.Page.HomePageModel).IsBusy = false;

                    IsEnabled = true;
                });
            });
        }
    }
}