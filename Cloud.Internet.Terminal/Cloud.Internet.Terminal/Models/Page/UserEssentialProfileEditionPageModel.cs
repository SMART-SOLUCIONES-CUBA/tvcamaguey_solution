namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Models.Page
{
    public class UserEssentialProfileEditionPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel, IProfileImageModeler
    {
        private string name;

        private System.Guid profileImageId;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public System.Guid ProfileImageId { get => profileImageId; set => SetProperty(ref profileImageId, value); }

        public string ProfileImageExtension { get; set; }
    }
}
