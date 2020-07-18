using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Pages
{
	[Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
	public partial class ShowDetailPage : XFToolKit.Mvvm.Pages.ContentPage, Facade.IImageContextBindingPageFacade
    {
		public ShowDetailPage(Cloud.Persistence.Entities.Show show)
		{
			InitializeComponent();

            IsEnabled = false;

			BindingContext = new Models.Page.ShowDetailPageModel
            {
                Show = show,
                IsBusy = true
			};
		}

        public SAPRESSC.Middleware.Media.Persistence.Entities.Image BindingContextImage => (BindingContext as Models.Page.ShowDetailPageModel)?.Show?.Poster;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (IsEnabled) return;

            IsEnabled = true;

            var pageBindingContext = BindingContext as Models.Page.ShowDetailPageModel;

            System.Threading.Tasks.Task.Run(() =>
            {
                var oldPresentations = new System.Collections.Generic.List<Cloud.Persistence.Entities.Presentation>();
                var nextPresentations = new System.Collections.Generic.List<Cloud.Persistence.Entities.Presentation>();

                using (var dbContext = Providers.DbContextProvider.Instance.GetDbContext())
                {
                    var now = System.DateTime.Now;

                    oldPresentations = dbContext.Presentations.Include(e => e.ProductionTeam).ThenInclude(e => e.EmployBridges).ThenInclude(b => b.Employ).ThenInclude(e => e.Identity).ThenInclude(e => e.Image).Where(e => e.ShowId == pageBindingContext.Show.Id && e.At < now).OrderBy(e => e.At).ToList();
                    nextPresentations = dbContext.Presentations.Include(e => e.ProductionTeam).ThenInclude(e => e.EmployBridges).ThenInclude(b => b.Employ).ThenInclude(e => e.Identity).ThenInclude(e => e.Image).Where(e => e.ShowId == pageBindingContext.Show.Id && e.At >= now).OrderBy(e => e.At).ToList();
                }

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    pageBindingContext.OldPresentations = oldPresentations;
                    pageBindingContext.NextPresentations = nextPresentations;

                    pageBindingContext.IsBusy = false;
                });
            });
        }
    }
}