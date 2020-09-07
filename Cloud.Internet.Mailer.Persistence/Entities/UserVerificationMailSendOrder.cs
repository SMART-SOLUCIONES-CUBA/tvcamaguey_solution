using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Mailer.Persistence.Entities
{
    public class UserVerificationMailSendOrder : SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        public UserVerificationMailSendOrder()
        {
            Id = System.Guid.NewGuid();
        }

        [Key]
        public System.Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
