namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Models.Page
{
    public class SessionConfigurationPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        public SessionConfigurationPageModel()
        {
            UserRegistrationPageModel = new UserRegistrationPageModel();
            UserAuthenticationPageModel = new UserAuthenticationPageModel();
            UserEssentialProfileEditionPageModel = new UserEssentialProfileEditionPageModel();
        }

        public UserRegistrationPageModel UserRegistrationPageModel { get; private set; }

        public UserAuthenticationPageModel UserAuthenticationPageModel { get; private set; }

        public UserEssentialProfileEditionPageModel UserEssentialProfileEditionPageModel { get; private set; }

        protected override void DisposeUnmanagementResources()
        {
            base.DisposeUnmanagementResources();

            UserRegistrationPageModel.Dispose();

            UserRegistrationPageModel = null;

            UserAuthenticationPageModel.Dispose();

            UserAuthenticationPageModel = null;

            UserEssentialProfileEditionPageModel.Dispose();

            UserEssentialProfileEditionPageModel = null;
        }
    }
}
