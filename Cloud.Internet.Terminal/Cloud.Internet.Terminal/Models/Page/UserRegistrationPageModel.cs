namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Models.Page
{
    public class UserRegistrationPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private string login;

        public string Login
        {
            get => login;
            set => SetProperty(ref login, value);
        }
    }
}
