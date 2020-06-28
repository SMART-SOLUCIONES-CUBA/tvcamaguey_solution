namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.EF.Repositories
{
    public class InstitutionInfoNomenclatorRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.InstitutionInfoNomenclator, Enumerators.InstitutionInfoKind>
    {
        public InstitutionInfoNomenclatorRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
