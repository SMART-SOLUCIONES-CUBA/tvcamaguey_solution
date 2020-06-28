namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Models.Page
{
    public class UserProfilePageModel : NavigationBlockPageModel, IProfileImageModeler
    {
        private string name;
        private string lastName;
        private System.DateTime bornDate;
        private SAPRESSC.Middleware.Identity.Persistence.Enumerations.CivilStatus civilStatus;
        private SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex sex;
        private string alias;
        private string about;
        private System.Guid profileImageId;

        public System.Guid Id { get; set; }

        public string Name { get => name; set => SetProperty(ref name, value); }

        public string LastName { get => lastName; set => SetProperty(ref lastName, value); }

        public System.DateTime BornDate { get => bornDate; set => SetProperty(ref bornDate, value); }

        public SAPRESSC.Middleware.Identity.Persistence.Enumerations.CivilStatus CivilStatus { get => civilStatus; set => SetProperty(ref civilStatus, value); }

        public SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex Sex { get => sex; set => SetProperty(ref sex, value); }

        public string Alias { get => alias; set => SetProperty(ref alias, value); }

        public string About { get => about; set => SetProperty(ref about, value); }

        public System.Guid ProfileImageId { get => profileImageId; set => SetProperty(ref profileImageId, value); }

        public string ProfileImageExtension { get; set; }
    }
}
