namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Models.Page
{
    public class MasterPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel, IProfileImageModeler
    {
        private string sessionLogin;
        private string sessionName;
        private System.Guid profileImageId;
        private string profileImageExtension;

        public System.Collections.Generic.List<NavigationGroupModel> NavigationSectionGroups { get; set; }

        public string SessionLogin { get => sessionLogin; set => SetProperty(ref sessionLogin, value); }

        public string SessionName { get => sessionName; set => SetProperty(ref sessionName, value); }

        public System.Guid ProfileImageId { get => profileImageId; set => SetProperty(ref profileImageId, value); }

        public string ProfileImageExtension { get => profileImageExtension; set => SetProperty(ref profileImageExtension, value); }

        protected override void DisposeUnmanagementResources()
        {
            base.DisposeUnmanagementResources();

            NavigationSectionGroups.Clear();

            NavigationSectionGroups = null;
        }
    }
}
