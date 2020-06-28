namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models
{
    public class RegistrationRequestModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.EmailAddress]
        public string Login { get; set; }
    }
}
