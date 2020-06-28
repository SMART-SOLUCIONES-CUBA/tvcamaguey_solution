namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models
{
    public class AuthenticationResponseModel
    {
        public string Token { get; set; }

        public string Type { get; set; }

        public int ExpiresInSecond { get; set; }

        public PersonIdentityDisplayModel Identity { get; set; }
    }
}
