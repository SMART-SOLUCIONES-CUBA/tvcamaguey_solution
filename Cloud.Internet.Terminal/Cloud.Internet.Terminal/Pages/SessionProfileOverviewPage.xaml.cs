using System.Linq;
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

            var pageBindingContext = BindingContext as Models.Page.UserProfilePageModel;

            if (!pageBindingContext.IsBusy) return;

            System.Threading.Tasks.Task.Run(async () =>
            {
                var hasRemoteChanges = await Managers.SessionManager.Instance.TryUpdateSessionIdentity();

                var identity = Managers.SessionManager.Instance.Identity;

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    if (hasRemoteChanges)
                    {
                        ((Xamarin.Forms.Application.Current.MainPage as MainPage).Master as MasterPage).InitializeContent(false);
                    }

                    pageBindingContext.Name = identity.Name;
                    pageBindingContext.LastName = identity.LastName;
                    pageBindingContext.Alias = identity.Alias;
                    pageBindingContext.BornDate = identity.BornDate;
                    pageBindingContext.CivilStatus = identity.CivilStatus;
                    pageBindingContext.Sex = identity.Sex;
                    pageBindingContext.About = identity.About;
                    pageBindingContext.ProfileImageExtension = identity.Image?.ContentType.ToImageExtension() ?? string.Empty;
                    pageBindingContext.ProfileImageId = identity.Image?.Id ?? System.Guid.Empty;

                    pageBindingContext.IsBusy = false;

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