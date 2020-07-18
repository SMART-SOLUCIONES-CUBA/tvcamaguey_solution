namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Terminal.Models.Page
{
    public class AboutUsPageModel : XFToolKit.Mvvm.Models.Page.BasePageModel
    {
        private string about;
        private string history;
        private string contactPhoneNumber;
        private string contactEmailAddress;
        private string contactPostalAddress;
        private double contactMapLatitude;
        private double contactMapLongitude;

        public string About { get => about; set => SetProperty(ref about, value); }

        public string History { get => history; set => SetProperty(ref history, value); }

        public string ContactPhoneNumber { get => contactPhoneNumber; set => SetProperty(ref contactPhoneNumber, value); }

        public string ContactEmailAddress { get => contactEmailAddress; set => SetProperty(ref contactEmailAddress, value); }

        public string ContactPostalAddress { get => contactPostalAddress; set => SetProperty(ref contactPostalAddress, value); }

        public double ContactMapLatitude { get => contactMapLatitude; set => SetProperty(ref contactMapLatitude, value); }

        public double ContactMapLongitude { get => contactMapLongitude; set => SetProperty(ref contactMapLongitude, value); }
    }
}
