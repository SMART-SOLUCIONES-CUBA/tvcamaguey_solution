using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Extensions;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
	public partial class MasterPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        private Models.NavigationSectionModel homeNavigationSection;
        private Models.NavigationSectionModel billboardNavigationSection;
        private Models.NavigationSectionModel newsNavigationSection;

        public MasterPage()
		{
			InitializeComponent();

            homeNavigationSection = new Models.NavigationSectionModel
            {
                Glyph = "\uf2de",
                Title = "Portada",
                SelectionFunc = () => new HomePage()
            };
            billboardNavigationSection = new Models.NavigationSectionModel
            {
                Glyph = "\ufdfa",
                Title = "Cartelera",
                SelectionFunc = () => new BillboardPage()
            };
            newsNavigationSection = new Models.NavigationSectionModel
            {
                Glyph = "\uf395",
                Title = "Noticias",
                SelectionFunc = () => new NewsPage()
            };

            var defaultNavigationGroup = new Models.NavigationGroupModel();

            var primaryNavigationGroup = new Models.NavigationGroupModel
            {
                Title = "Televisión Camagüey"
            };

            var secoundaryNavigationGroup = new Models.NavigationGroupModel
            {
                Title = "Aplicación"
            };

            defaultNavigationGroup.Add(homeNavigationSection);

            primaryNavigationGroup.Add(new Models.NavigationSectionModel
            {
                Glyph = "\ue900",
                FontFamily = Xamarin.Forms.Device.RuntimePlatform != Xamarin.Forms.Device.UWP ? "IcoMoon" : "/Assets/Fonts/icomoon.ttf#icomoon",
                Title = "Acerca de",
                SelectionFunc = () => new AboutUsPage()
            });
            primaryNavigationGroup.Add(billboardNavigationSection);
            primaryNavigationGroup.Add(new Models.NavigationSectionModel
            {
                Glyph = "\ufcde",
                Title = "Programas",
                SelectionFunc = () => new ShowsPage()
            });
            primaryNavigationGroup.Add(newsNavigationSection);
            primaryNavigationGroup.Add(new Models.NavigationSectionModel
            {
                Glyph = "\ufcf5",
                Title = "Video Reportages",
                SelectionFunc = () => new AudioVisualsPage()
            });

            secoundaryNavigationGroup.Add(new Models.NavigationSectionModel
            {
                Glyph = "\uf78a",
                Title = "Información y Ayuda",
                SelectionFunc = () => new SupportPage()
            });
            secoundaryNavigationGroup.Add(new Models.NavigationSectionModel
            {
                Glyph = "\uf009",
                Title = "Perfil de Usuario",
                SelectionFunc = () => new SessionProfileOverviewPage()
            });
            secoundaryNavigationGroup.Add(new Models.NavigationSectionModel
            {
                Glyph = "\uf5fd",
                Title = "Cerrar Sesión",
                SelectionFunc = () => new CloseSessionPage()
            });

            BindingContext = new Models.Page.MasterPageModel
            {
                IsBusy = true,
                NavigationSectionGroups = new System.Collections.Generic.List<Models.NavigationGroupModel>
                {
                    defaultNavigationGroup,
                    primaryNavigationGroup,
                    secoundaryNavigationGroup
                }
            };
        }

        internal void PresentHomeSection()
        {
            NavigationListView.SelectedItem = homeNavigationSection;
        }

        internal void PresentBillboardSection()
        {
            NavigationListView.SelectedItem = billboardNavigationSection;
        }

        internal void PresentNewsSection()
        {
            NavigationListView.SelectedItem = newsNavigationSection;
        }

        public void InitializeContent(bool autoShow = false)
        {
            var pageModel = BindingContext as Models.Page.MasterPageModel;

            System.Threading.Tasks.Task.Run(() =>
            {
                var identity = Managers.SessionManager.Instance.Identity;

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    pageModel.SessionLogin = Managers.SessionManager.Instance.Login;
                    pageModel.SessionName = string.IsNullOrEmpty(identity.LastName) ? identity.Name : string.Format("{0} {1}", identity.Name, identity.LastName);
                    pageModel.ProfileImageExtension = identity.Image?.ContentType.ToImageExtension() ?? string.Empty;
                    pageModel.ProfileImageId = identity.Image?.Id ?? System.Guid.Empty;

                    pageModel.IsBusy = false;

                    identity = null;

                    if (autoShow)
                    {
                        (Parent as Xamarin.Forms.MasterDetailPage).IsPresented = true;
                    }
                });
            });
        }

        private void NavigationListView_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if ((Parent as Xamarin.Forms.MasterDetailPage).Detail is Xamarin.Forms.NavigationPage)
            {
                ((Parent as Xamarin.Forms.MasterDetailPage).Detail as Xamarin.Forms.NavigationPage).PopToRootAsync(false);
            }            

            var outgoingPage = (Parent as Xamarin.Forms.MasterDetailPage).Detail;

            var incomingNavigationPage = new Xamarin.Forms.NavigationPage((e.SelectedItem as Models.NavigationSectionModel).SelectionFunc.Invoke());

            (Parent as Xamarin.Forms.MasterDetailPage).Detail = incomingNavigationPage;

            if (outgoingPage is System.IDisposable)
            {
                (outgoingPage as System.IDisposable).Dispose();
            }

            System.Threading.Tasks.Task.Delay(System.TimeSpan.FromMilliseconds(100)).ContinueWith(task =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    (Parent as Xamarin.Forms.MasterDetailPage).IsPresented = false;
                });
            });
        }

        protected override void DisposeUnmanagementResources()
        {
            base.DisposeUnmanagementResources();

            NavigationListView.Header = null;

            homeNavigationSection = null;
        }
    }
}