namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Models.Page
{
    public class UserAuthenticationPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private string login;
        private string token;

        public string Login
        {
            get => login;
            set => SetProperty(ref login, value);
        }

        public string Purpose { get; set; }

        public string Token
        {
            get => token;
            set => SetProperty(ref token, value);
        }
    }
}
