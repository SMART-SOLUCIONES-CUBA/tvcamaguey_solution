using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Pages
{
	[Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
	public partial class PresentationDetailPage : XFToolKit.Mvvm.Pages.ContentPage
    {
		public PresentationDetailPage(Cloud.Persistence.Entities.Presentation presentation)
		{
			InitializeComponent();

			IsEnabled = false;

			BindingContext = new Models.Page.PresentationDetailPageModel
			{
                Id = presentation.Id,
                At = presentation.At,
                About = presentation.About,
                ProductionTeamId = presentation.ProductionTeamId,
                Show = presentation.Show,
                IsBusy = true
			};
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (IsEnabled) return;

            IsEnabled = true;

            var pageBindingContext = BindingContext as Models.Page.PresentationDetailPageModel;

            System.Threading.Tasks.Task.Run(() =>
            {
                Cloud.Persistence.Entities.ProductionTeam result = null;

                using (var dbContext = Providers.DbContextProvider.Instance.GetDbContext())
                {
                    result = dbContext.ProductionTeams.Include(e => e.EmployBridges).ThenInclude(b => b.Employ).ThenInclude(e => e.Identity).ThenInclude(e => e.Image).First(e => e.Id == pageBindingContext.ProductionTeamId);
                }

                return result;
            }).ContinueWith(task =>
            {
                var conductionEmploies = task.Result.EmployBridges.Where(b => b.ProductionRole == Cloud.Persistence.Enumerations.ProductionRole.Conduction).Select(b => b.Employ).ToList();
                var directionEmploies = task.Result.EmployBridges.Where(b => b.ProductionRole == Cloud.Persistence.Enumerations.ProductionRole.Direction).Select(b => b.Employ).ToList();
                var productionEmploies = task.Result.EmployBridges.Where(b => b.ProductionRole == Cloud.Persistence.Enumerations.ProductionRole.Production).Select(b => b.Employ).ToList();

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    if (conductionEmploies.Count > 0)
                    {
                        pageBindingContext.ConductionEmploies = conductionEmploies;
                    }

                    if (directionEmploies.Count > 0)
                    {
                        pageBindingContext.DirectionEmploies = directionEmploies;
                    }

                    if (productionEmploies.Count > 0)
                    {
                        pageBindingContext.ProductionEmploies = productionEmploies;
                    }

                    pageBindingContext.IsBusy = false;
                });
            });
        }
    }
}