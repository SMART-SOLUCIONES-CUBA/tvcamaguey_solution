using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.SAPRESSC.Core.Web.SecurityLocal.Extensions;
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

            var roleDbSet = dbContext.Set<SAPRESSC.Core.Security.Persistence.Entities.Role>();

            #region Employ Roles definitios

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.EmployViewer)))
            {
                roleDbSet.Add(Security.Authorization.Roles.EmployViewer.ToRoleEntity());
            }

            #endregion

            #region Show Roles definitios

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.ShowViewer)))
            {
                roleDbSet.Add(Security.Authorization.Roles.ShowViewer.ToRoleEntity());
            }

            # endregion

            #region Award Roles definitios

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.AwardViewer)))
            {
                roleDbSet.Add(Security.Authorization.Roles.AwardViewer.ToRoleEntity());
            }

            #endregion

            #region Blog Roles definitios

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.BlogViewer)))
            {
                roleDbSet.Add(Security.Authorization.Roles.BlogViewer.ToRoleEntity());
            }

            #endregion

            #region ProductionTeam Roles definitios

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.ProductionTeamViewer)))
            {
                roleDbSet.Add(Security.Authorization.Roles.ProductionTeamViewer.ToRoleEntity());
            }

            #endregion

            #region Presentation Roles definitios

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.PresentationViewer)))
            {
                roleDbSet.Add(Security.Authorization.Roles.PresentationViewer.ToRoleEntity());
            }

            #endregion

            #region Category Roles definitios

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.CategoryViewer)))
            {
                roleDbSet.Add(Security.Authorization.Roles.CategoryViewer.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.CategoryCreator)))
            {
                roleDbSet.Add(Security.Authorization.Roles.CategoryCreator.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.CategoryEditor)))
            {
                roleDbSet.Add(Security.Authorization.Roles.CategoryEditor.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.CategoryRemover)))
            {
                roleDbSet.Add(Security.Authorization.Roles.CategoryRemover.ToRoleEntity());
            }

            #endregion

            #region Tag Roles definitios

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.TagViewer)))
            {
                roleDbSet.Add(Security.Authorization.Roles.TagViewer.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.TagCreator)))
            {
                roleDbSet.Add(Security.Authorization.Roles.TagCreator.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.TagEditor)))
            {
                roleDbSet.Add(Security.Authorization.Roles.TagEditor.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.TagRemover)))
            {
                roleDbSet.Add(Security.Authorization.Roles.TagRemover.ToRoleEntity());
            }

            #region Post Roles definitios

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.PostViewer)))
            {
                roleDbSet.Add(Security.Authorization.Roles.PostViewer.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.PostCreator)))
            {
                roleDbSet.Add(Security.Authorization.Roles.PostCreator.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.PostEditor)))
            {
                roleDbSet.Add(Security.Authorization.Roles.PostEditor.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.PostRemover)))
            {
                roleDbSet.Add(Security.Authorization.Roles.PostRemover.ToRoleEntity());
            }

            #endregion

            #endregion

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
