namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.EF.Repositories
{
    public class InstitutionBusinessUpdateRecordRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.InstitutionBusinessUpdateRecord, long>
    {
        public InstitutionBusinessUpdateRecordRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
