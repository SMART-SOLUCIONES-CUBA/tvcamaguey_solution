using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View
{
    public class InstitutionInfoDisplayModel
    {
        public string AboutUs { get; set; }

        public string History { get; set; }

        public string ContactPhoneNumber { get; set; }

        public string ContactEmailAddress { get; set; }

        public string ContactPostalAddress { get; set; }

        public double ContactMapLatitude { get; set; }

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
