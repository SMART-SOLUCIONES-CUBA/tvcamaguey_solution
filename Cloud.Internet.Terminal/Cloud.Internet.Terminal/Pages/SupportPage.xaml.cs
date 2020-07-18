using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
	public partial class SupportPage : XFToolKit.Mvvm.Pages.ContentPage
    {
		public SupportPage()
		{
			InitializeComponent();

			IsEnabled = false;

			BindingContext = new Models.Page.SupportPageModel
			{
				IsBusy = true
			};
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (IsEnabled) return;

			var pageBindingContext = BindingContext as Models.Page.SupportPageModel;

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

		private void VendorWebSiteButton_Clicked(object sender, System.EventArgs args)
		{
			Xamarin.Essentials.Launcher.OpenAsync(new System.Uri("http://www.tvcamaguey.icrt.cu"));
		}

		private void VendorPhoneButton_Clicked(object sender, System.EventArgs args)
		{
			Xamarin.Essentials.Launcher.OpenAsync(new System.Uri(string.Format("tel:{0}", (BindingContext as Models.Page.SupportPageModel).ContactPhoneNumber)));
		}

		private void VendorEmailButton_Clicked(object sender, System.EventArgs args)
		{
			Xamarin.Essentials.Launcher.OpenAsync(new System.Uri(string.Format("mailto:{0}", (BindingContext as Models.Page.SupportPageModel).ContactEmailAddress)));
		}

		private void VendorMapButton_Clicked(object sender, System.EventArgs args)
		{
			var pageBindingContext = BindingContext as Models.Page.SupportPageModel;

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

		private void VendorYoutubeButton_Clicked(object sender, System.EventArgs args)
		{
			Xamarin.Essentials.Launcher.OpenAsync(new System.Uri("https://www.youtube.com/user/tvcamaguey"));
		}

		private void VendorFacebookButton_Clicked(object sender, System.EventArgs args)
		{
			Xamarin.Essentials.Launcher.OpenAsync(new System.Uri("https://www.facebook.com/camagueytv"));
		}

		private void VendorInstagramButton_Clicked(object sender, System.EventArgs args)
		{
			Xamarin.Essentials.Launcher.OpenAsync(new System.Uri("https://www.instagram.com/tvcamaguey/"));
		}

		private void VendorTwitterButton_Clicked(object sender, System.EventArgs args)
		{
			Xamarin.Essentials.Launcher.OpenAsync(new System.Uri("https://twitter.com/tvcamaguey"));
		}

		private void OwnerWebSiteButton_Clicked(object sender, System.EventArgs args)
        {
			Xamarin.Essentials.Launcher.OpenAsync(new System.Uri("http://www.smartsolucionescuba.com"));
		}

		private void OwnerPhoneButton_Clicked(object sender, System.EventArgs args)
		{
			Xamarin.Essentials.Launcher.OpenAsync(new System.Uri("tel:+5353201929"));
		}

		private void OwnerEmailButton_Clicked(object sender, System.EventArgs args)
		{
			Xamarin.Essentials.Launcher.OpenAsync(new System.Uri("mailto:contacto@smartsolucionescuba.com"));
		}

		private void OwnerFacebookButton_Clicked(object sender, System.EventArgs args)
		{
			Xamarin.Essentials.Launcher.OpenAsync(new System.Uri("https://www.facebook.com/smartsolucionescuba"));
		}

		private void OwnerTelegramButton_Clicked(object sender, System.EventArgs args)
		{
			Xamarin.Essentials.Launcher.OpenAsync(new System.Uri("https://t.me/smartsolucionescuba"));
		}

		private void OwnerTwitterButton_Clicked(object sender, System.EventArgs args)
		{
			Xamarin.Essentials.Launcher.OpenAsync(new System.Uri("https://twitter.com/smartSoluc_cuba"));
		}
	}
}