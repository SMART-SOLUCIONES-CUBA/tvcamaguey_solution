namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models
{
    public class AuthenticationRequestModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.EmailAddress]
        public string Login { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Purpose { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Token { get; set; }
    }
}
