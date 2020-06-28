using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Extensions;
using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Pages
{
	[Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
	public partial class SessionProfileOverviewPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        public SessionProfileOverviewPage()
		{
			InitializeComponent();

            BindingContext = new Models.Page.UserProfilePageModel { IsBusy = true };
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var pageModel = BindingContext as Models.Page.UserProfilePageModel;

            if (!pageModel.IsBusy) return;

            System.Threading.Tasks.Task.Run(() =>
            {
                var identity = Managers.SessionManager.Instance.Identity;

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    pageModel.Name = identity.Name;
                    pageModel.LastName = identity.LastName;
                    pageModel.Alias = identity.Alias;
                    pageModel.BornDate = identity.BornDate;
                    pageModel.CivilStatus = identity.CivilStatus;
                    pageModel.Sex = identity.Sex;
                    pageModel.About = identity.About;
                    pageModel.ProfileImageExtension = identity.Image?.ContentType.ToImageExtension() ?? string.Empty;
                    pageModel.ProfileImageId = identity.Image?.Id ?? System.Guid.Empty;

                    pageModel.IsBusy = false;

                    OnPropertyChanged(WidthProperty.PropertyName);
                });
            });
        }

        private void EditToolbarItem_Clicked(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPushPageAsync(new SessionProfileEditPage(BindingContext as Models.Page.UserProfilePageModel));
        }
    }
}