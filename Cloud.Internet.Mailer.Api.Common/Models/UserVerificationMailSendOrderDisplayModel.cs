namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Mailer.Api.Common.Models
{
    public class UserVerificationMailSendOrderDisplayModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.UserVerificationMailSendOrder, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string UserId { get; set; }

        public string Token { get; set; }

        public void Import(Persistence.Entities.UserVerificationMailSendOrder entity)
        {
            Id = entity.Id;
            UserId = entity.UserId;
            Token = entity.Token;
        }
    }
}
