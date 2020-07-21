using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View
{
    public class InstitutionInfoInputModel
    {
        [Required, Display(Name = "AboutUs")]
        public string AboutUs { get; set; }

        [Required, Display(Name = "History")]
        public string History { get; set; }

        [Required, Phone, Display(Name = "Phone")]
        public string ContactPhoneNumber { get; set; }

        [Required, EmailAddress, Display(Name = "Email")]
        public string ContactEmailAddress { get; set; }

        [Required, Display(Name = "PostalAddress")]
        public string ContactPostalAddress { get; set; }

        [Required, Display(Name = "Latitude")]
        public double ContactMapLatitude { get; set; }

        [Required, Display(Name = "Longitude")]
        public double ContactMapLongitude { get; set; }

        public void Import(System.Collections.Generic.Dictionary<Persistence.Enumerators.InstitutionInfoKind, string> keyValuePairs)
        {
            AboutUs = keyValuePairs[Persistence.Enumerators.InstitutionInfoKind.AboutUs];
            History = keyValuePairs[Persistence.Enumerators.InstitutionInfoKind.History];
            ContactPhoneNumber = keyValuePairs[Persistence.Enumerators.InstitutionInfoKind.ContactPhoneNumber];
            ContactEmailAddress = keyValuePairs[Persistence.Enumerators.InstitutionInfoKind.ContactEmailAddress];
            ContactPostalAddress = keyValuePairs[Persistence.Enumerators.InstitutionInfoKind.ContactPostalAddress];
            ContactMapLatitude = double.Parse(keyValuePairs[Persistence.Enumerators.InstitutionInfoKind.ContactMapLatitude]);
            ContactMapLongitude = double.Parse(keyValuePairs[Persistence.Enumerators.InstitutionInfoKind.ContactMapLongitude]);
        }
    }
}
