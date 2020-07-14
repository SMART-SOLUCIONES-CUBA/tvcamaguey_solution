using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Pages
{
    public partial class MainPage : XFToolKit.Mvvm.Pages.MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            IsEnabled = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!Managers.SessionManager.Instance.IsAuthenticated)
            {
                this.NavigationPushModalPageAsync(new SessionConfigurationPage());

                IsEnabled = false;
            }
            else if (!Managers.SessionManager.Instance.IsCompleteProfiled)
            {
                Managers.SessionManager.Instance.CloseAsync().ContinueWith(_ =>
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        this.NavigationPushModalPageAsync(new SessionConfigurationPage());

                        IsEnabled = false;
                    });
                });
            }
            else if (!IsEnabled)
            {
                IsEnabled = true;

                Managers.SessionManager.Instance.TryUpdateSessionIdentity().ContinueWith(_ =>
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        (Master as MasterPage).InitializeContent(false);

                        (Master as MasterPage).PresentHomeSection();
                    });
                });
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if ((Detail as Xamarin.Forms.NavigationPage).CurrentPage == (Detail as Xamarin.Forms.NavigationPage).RootPage)
            {
                if ((Detail as Xamarin.Forms.NavigationPage).CurrentPage is HomePage)
                {
                    DisplayAlert("Aviso", "Está seguro que desea salir de la aplicación?", "Salir", "Cancelar").ContinueWith((System.Threading.Tasks.Task<bool> task) =>
                    {
                        if (task.Result)
                        {
                            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                            {
                                Xamarin.Forms.Application.Current.Quit();
                            });
                        }
                    });
                }
                else
                {
                    (Master as MasterPage).PresentHomeSection();
                }

                return true;
            }

            return base.OnBackButtonPressed();
        }
    }
}