using System.Linq;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Extensions;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Pages
{
	[Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
	public partial class AboutUsPage : XFToolKit.Mvvm.Pages.TabbedPage
    {
		public AboutUsPage()
		{
			InitializeComponent();

            IsEnabled = false;

            BindingContext = new Models.Page.AboutUsPageModel
            {
                IsBusy = true
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (IsEnabled) return;

            var pageBindingContext = BindingContext as Models.Page.AboutUsPageModel;

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
                    pageBindingContext.About = task.Result.First(e => e.Id == Internet.Persistence.Enumerators.InstitutionInfoKind.AboutUs).Value.ToStandarizedHtmlContent();
                    pageBindingContext.History = task.Result.First(e => e.Id == Internet.Persistence.Enumerators.InstitutionInfoKind.History).Value.ToStandarizedHtmlContent();
                    pageBindingContext.ContactPhoneNumber = task.Result.First(e => e.Id == Internet.Persistence.Enumerators.InstitutionInfoKind.ContactPhoneNumber).Value;
                    pageBindingContext.ContactEmailAddress = task.Result.First(e => e.Id == Internet.Persistence.Enumerators.InstitutionInfoKind.ContactEmailAddress).Value;
                    pageBindingContext.ContactPostalAddress = task.Result.First(e => e.Id == Internet.Persistence.Enumerators.InstitutionInfoKind.ContactPostalAddress).Value;
                    pageBindingContext.ContactMapLatitude = double.Parse(task.Result.First(e => e.Id == Internet.Persistence.Enumerators.InstitutionInfoKind.ContactMapLatitude).Value);
                    pageBindingContext.ContactMapLongitude = double.Parse(task.Result.First(e => e.Id == Internet.Persistence.Enumerators.InstitutionInfoKind.ContactMapLongitude).Value);

                    pageBindingContext.IsBusy = false;
                });
            });

            IsEnabled = true;
        }

        private void VendorPhoneButton_Clicked(object sender, System.EventArgs args)
        {
            Xamarin.Essentials.Launcher.OpenAsync(new System.Uri(string.Format("tel:{0}", (BindingContext as Models.Page.AboutUsPageModel).ContactPhoneNumber)));
        }

        private void VendorEmailButton_Clicked(object sender, System.EventArgs args)
        {
            Xamarin.Essentials.Launcher.OpenAsync(new System.Uri(string.Format("mailto:{0}", (BindingContext as Models.Page.AboutUsPageModel).ContactEmailAddress)));
        }

        private void VendorMapButton_Clicked(object sender, System.EventArgs args)
        {
            var pageBindingContext = BindingContext as Models.Page.AboutUsPageModel;

            var name = System.Uri.EscapeUriString("Televisión Camagüey");
            var loc = string.Format("{0},{1}", pageBindingContext.ContactMapLatitude.ToString(System.Globalization.CultureInfo.InvariantCulture), pageBindingContext.ContactMapLatitude.ToString(System.Globalization.CultureInfo.InvariantCulture));
            var addr = System.Uri.EscapeUriString(pageBindingContext.ContactPostalAddress);

            var launchMapUri = new System.Uri(string.Format("bingmaps:?cp={0}&q={1}", loc, name));

            switch (Xamarin.Forms.Device.RuntimePlatform)
            {
                case Xamarin.Forms.Device.iOS:
                case Xamarin.Forms.Device.macOS:
                    launchMapUri = new System.Uri(string.Format("http://maps.apple.com/maps?q={0}&sll={1}", name.Replace(' ', '+'), loc));

                    break;
                case Xamarin.Forms.Device.Android:
                    launchMapUri = new System.Uri(string.Format("geo:{0}?q={1}({2})", loc, string.IsNullOrWhiteSpace(addr) ? loc : addr, name));

                    break;
            }

            Xamarin.Essentials.Launcher.OpenAsync(launchMapUri);
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
                    var model = BindingContext as Models.Page.AboutUsPageModel;

                    if (task.Exception != null || task.IsCanceled)
                    {
                        DisplayAlert("Aviso", "No fue posible actualizar la información de Televisión Camagüey en este momento. Revise la conexión Red Wi-Fi o Datos Móviles de su dispositivo.", "Aceptar");
                    }
                    else
                    {
                        model.About = task.Result.First(e => e.Id == Internet.Persistence.Enumerators.InstitutionInfoKind.AboutUs).Value.ToStandarizedHtmlContent();
                        model.History = task.Result.First(e => e.Id == Internet.Persistence.Enumerators.InstitutionInfoKind.History).Value.ToStandarizedHtmlContent();
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