using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Pages
{
	[Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
	public partial class AboutUsPage : XFToolKit.Mvvm.Pages.TabbedPage
    {
		public AboutUsPage()
		{
			InitializeComponent();

            IsEnabled = false;

            BindingContext = new Models.Page.AboutUsPageModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (IsEnabled) return;

            (BindingContext as Models.Page.AboutUsPageModel).IsBusy = true;

            System.Threading.Tasks.Task.Run(() =>
            {
                var institutionInfoNomenclators = new System.Collections.Generic.List<Internet.Persistence.Entities.InstitutionInfoNomenclator>();

                using (var dbContext = Providers.DbContextProvider.Instance.GetDbContext())
                {
                    institutionInfoNomenclators = dbContext.InstitutionInfoNomenclators.ToList();
                }

                return institutionInfoNomenclators;
            }).ContinueWith((System.Threading.Tasks.Task<System.Collections.Generic.List<Internet.Persistence.Entities.InstitutionInfoNomenclator>> task) =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    var model = (BindingContext as Models.Page.AboutUsPageModel);

                    model.About = new Xamarin.Forms.HtmlWebViewSource { Html = string.Format("<html><body style=\"margin: 10pt;\">{0}</body></html>", task.Result.First(e => e.Id == Internet.Persistence.Enumerators.InstitutionInfoKind.AboutUs).Value) };
                    model.History = new Xamarin.Forms.HtmlWebViewSource { Html = string.Format("<html><body style=\"margin: 10pt;\">{0}</body></html>", task.Result.First(e => e.Id == Internet.Persistence.Enumerators.InstitutionInfoKind.History).Value) };
                    model.ContactPhoneNumber = task.Result.First(e => e.Id == Internet.Persistence.Enumerators.InstitutionInfoKind.ContactPhoneNumber).Value;
                    model.ContactEmailAddress = task.Result.First(e => e.Id == Internet.Persistence.Enumerators.InstitutionInfoKind.ContactEmailAddress).Value;
                    model.ContactPostalAddress = task.Result.First(e => e.Id == Internet.Persistence.Enumerators.InstitutionInfoKind.ContactPostalAddress).Value;

                    model.IsBusy = false;
                });
            });

            IsEnabled = true;
        }

        private void UpdateToolbarItem_Clicked(object sender, System.EventArgs eventArgs)
        {
            if ((BindingContext as Models.Page.AboutUsPageModel).IsBusy) return;

            (BindingContext as Models.Page.AboutUsPageModel).IsBusy = true;

            System.Threading.Tasks.Task.Run(async () =>
            {
                var client = new Api.Connect.Clients.InstitutionInfoNomenclatorsApiClient(Providers.HttpClientHelperProvider.Instance);

                var models = await client.GetAsync();

                var result = new System.Collections.Generic.List<Internet.Persistence.Entities.InstitutionInfoNomenclator>();

                using (var dbContext = Providers.DbContextProvider.Instance.GetDbContext())
                {
                    dbContext.InstitutionInfoNomenclators.UpdateRange(models.Select(m => new Internet.Persistence.Entities.InstitutionInfoNomenclator
                    {
                        Id = m.Id,
                        Value = m.Value
                    }));

                    dbContext.SaveChanges(string.Empty, syncronize: false);

                    result = dbContext.InstitutionInfoNomenclators.ToList();
                }

                return result;
            }).ContinueWith((System.Threading.Tasks.Task<System.Collections.Generic.List<Internet.Persistence.Entities.InstitutionInfoNomenclator>> task) =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    var model = (BindingContext as Models.Page.AboutUsPageModel);

                    if (task.Exception != null || task.IsCanceled)
                    {
                        DisplayAlert("Aviso", "No fue posible actualizar la información de Televisión Camagüey en este momento. Revise la conexión Red Wi-Fi o Datos Móviles de su dispositivo.", "Aceptar");
                    }
                    else
                    {
                        model.About = new Xamarin.Forms.HtmlWebViewSource { Html = string.Format("<html><body style=\"margin: 10pt;\">{0}</body></html>", task.Result.First(e => e.Id == Internet.Persistence.Enumerators.InstitutionInfoKind.AboutUs).Value) };
                        model.History = new Xamarin.Forms.HtmlWebViewSource { Html = string.Format("<html><body style=\"margin: 10pt;\">{0}</body></html>", task.Result.First(e => e.Id == Internet.Persistence.Enumerators.InstitutionInfoKind.History).Value) };
                        model.ContactPhoneNumber = task.Result.First(e => e.Id == Internet.Persistence.Enumerators.InstitutionInfoKind.ContactPhoneNumber).Value;
                        model.ContactEmailAddress = task.Result.First(e => e.Id == Internet.Persistence.Enumerators.InstitutionInfoKind.ContactEmailAddress).Value;
                        model.ContactPostalAddress = task.Result.First(e => e.Id == Internet.Persistence.Enumerators.InstitutionInfoKind.ContactPostalAddress).Value;
                    }

                    model.IsBusy = false;
                });
            });
        }
    }
}