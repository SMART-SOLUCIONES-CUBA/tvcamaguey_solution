namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Models.Page
{
    public class NavigationBlockPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        public new bool IsBusy
        {
            get { return busy; }
            set
            {
                SetProperty(ref busy, value, () =>
                {
                    ((Xamarin.Forms.Application.Current.MainPage as Pages.MainPage).Master as Pages.MasterPage).IsEnabled = !busy;
                });
            }
        }
    }
}
