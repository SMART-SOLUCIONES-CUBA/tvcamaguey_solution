namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.EF.Repositories
{
    public class UserVerificationMailSendOrderRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Mailer.Persistence.Entities.UserVerificationMailSendOrder, System.Guid>
    {
        public UserVerificationMailSendOrderRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
