using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Pages
{
	[Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
	public partial class CloseSessionPage : XFToolKit.Mvvm.Pages.ContentPage
    {
		public CloseSessionPage()
		{
			InitializeComponent();

            BindingContext = new XFToolKit.Mvvm.Models.Page.BasePageModel();
        }

        private void CloseSessionButton_Clicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Confirmar", "Esta seguro que desea cerrar sesión y con ello eliminar todos los datos en la aplicación?", "Cerrar Sesión", "Cancelar").ContinueWith((System.Threading.Tasks.Task<bool> alertTask) =>
            {
                if (!alertTask.Result) return;

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    (BindingContext as XFToolKit.Mvvm.Models.Page.BasePageModel).IsBusy = true;

                    Managers.SessionManager.Instance.CloseAsync().ContinueWith((System.Threading.Tasks.Task task) =>
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            (BindingContext as XFToolKit.Mvvm.Models.Page.BasePageModel).IsBusy = false;

                            ((Xamarin.Forms.Application.Current.MainPage as MainPage).Master as MasterPage).PresentHomeSection();

                            if (!Managers.SessionManager.Instance.IsAuthenticated)
                            {
                                Xamarin.Forms.Application.Current.MainPage.NavigationPushModalPageAsync(new SessionConfigurationPage());
                            }
                            else
                            {
                                throw new System.InvalidOperationException();
                            }
                        });
                    });
                });
            });
        }
    }
}