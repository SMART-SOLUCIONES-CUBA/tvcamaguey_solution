using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Mailer.Api.Common.Models
{
    public class UserVerificationMailSendOrderInputModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Persistence.Entities.UserVerificationMailSendOrder, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }

        public Persistence.Entities.UserVerificationMailSendOrder Export()
        {
            var entity = new Persistence.Entities.UserVerificationMailSendOrder();

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.UserVerificationMailSendOrder entity)
        {
            Id = entity.Id;
            UserId = entity.UserId;
            Token = entity.Token;
        }

        public void Mergue(Persistence.Entities.UserVerificationMailSendOrder entity)
        {
            entity.UserId = UserId;
            entity.Token = Token;
        }
    }
}
