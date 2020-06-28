namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.EF.Repositories
{
    public class InstitutionInfoUpdateRecordRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.InstitutionInfoUpdateRecord, long>
    {
        public InstitutionInfoUpdateRecordRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
