using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Helpers
{
    public class DataInitializationHelper : SAPRESSC.Core.Persistence.EF.Helpers.Facade.IDataInitializationHelper
    {
        public void InitialiseData(DbContext dbContext)
        {
            var institutionInfoNomenclatorSet = dbContext.Set<Persistence.Entities.InstitutionInfoNomenclator>();

            if (!institutionInfoNomenclatorSet.Any(e => e.Id == Persistence.Enumerators.InstitutionInfoKind.AboutUs))
            {
                institutionInfoNomenclatorSet.Add(new Persistence.Entities.InstitutionInfoNomenclator
                {
                    Id = Persistence.Enumerators.InstitutionInfoKind.AboutUs,
                    Value = "A cerca de Televición Camaguey ..."
                });
            }

            if (!institutionInfoNomenclatorSet.Any(e => e.Id == Persistence.Enumerators.InstitutionInfoKind.History))
            {
                institutionInfoNomenclatorSet.Add(new Persistence.Entities.InstitutionInfoNomenclator
                {
                    Id = Persistence.Enumerators.InstitutionInfoKind.History,
                    Value = "Historia de Televición Camaguey ..."
                });
            }

            if (!institutionInfoNomenclatorSet.Any(e => e.Id == Persistence.Enumerators.InstitutionInfoKind.ContactPhoneNumber))
            {
                institutionInfoNomenclatorSet.Add(new Persistence.Entities.InstitutionInfoNomenclator
                {
                    Id = Persistence.Enumerators.InstitutionInfoKind.ContactPhoneNumber,
                    Value = "(+35) 32222121"
                });
            }

            if (!institutionInfoNomenclatorSet.Any(e => e.Id == Persistence.Enumerators.InstitutionInfoKind.ContactEmailAddress))
            {
                institutionInfoNomenclatorSet.Add(new Persistence.Entities.InstitutionInfoNomenclator
                {
                    Id = Persistence.Enumerators.InstitutionInfoKind.ContactEmailAddress,
                    Value = "contacto@tvcamaguey.icrt.cu"
                });
            }

            if (!institutionInfoNomenclatorSet.Any(e => e.Id == Persistence.Enumerators.InstitutionInfoKind.ContactPostalAddress))
            {
                institutionInfoNomenclatorSet.Add(new Persistence.Entities.InstitutionInfoNomenclator
                {
                    Id = Persistence.Enumerators.InstitutionInfoKind.ContactPostalAddress,
                    Value = "Ave. Revolución #28, Camaguey, Camaguey, Cuba."
                });
            }

            if (!institutionInfoNomenclatorSet.Any(e => e.Id == Persistence.Enumerators.InstitutionInfoKind.ContactMapLatitude))
            {
                institutionInfoNomenclatorSet.Add(new Persistence.Entities.InstitutionInfoNomenclator
                {
                    Id = Persistence.Enumerators.InstitutionInfoKind.ContactMapLatitude,
                    Value = "21212312312"
                });
            }

            if (!institutionInfoNomenclatorSet.Any(e => e.Id == Persistence.Enumerators.InstitutionInfoKind.ContactMapLongitude))
            {
                institutionInfoNomenclatorSet.Add(new Persistence.Entities.InstitutionInfoNomenclator
                {
                    Id = Persistence.Enumerators.InstitutionInfoKind.ContactMapLongitude,
                    Value = "1231212"
                });
            }

            // TODO Implement data initialization

            if (dbContext is SAPRESSC.Core.Persistence.EF.Context.ITraceableDbContext)
            {
                (dbContext as SAPRESSC.Core.Persistence.EF.Context.ITraceableDbContext).SaveChanges("system");
            }
            else
            {
                dbContext.SaveChanges();
            }
        }
    }
}
