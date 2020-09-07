using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.SAPRESSC.Core.Web.SecurityLocal.Extensions;
using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Helpers
{
    public class DataInitializationHelper : SAPRESSC.Core.Persistence.EF.Helpers.Facade.IDataInitializationHelper
    {
        public void InitialiseData(DbContext dbContext)
        {
            var roleDbSet = dbContext.Set<SAPRESSC.Core.Security.Persistence.Entities.Role>();

            #region Employ Roles definitios

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.EmployViewer)))
            {
                roleDbSet.Add(Security.Authorization.Roles.EmployViewer.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.EmployCreator)))
            {
                roleDbSet.Add(Security.Authorization.Roles.EmployCreator.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.EmployEditor)))
            {
                roleDbSet.Add(Security.Authorization.Roles.EmployEditor.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.EmployRemover)))
            {
                roleDbSet.Add(Security.Authorization.Roles.EmployRemover.ToRoleEntity());
            }

            #endregion

            #region Show Roles definitios

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.ShowViewer)))
            {
                roleDbSet.Add(Security.Authorization.Roles.ShowViewer.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.ShowCreator)))
            {
                roleDbSet.Add(Security.Authorization.Roles.ShowCreator.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.ShowEditor)))
            {
                roleDbSet.Add(Security.Authorization.Roles.ShowEditor.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.ShowRemover)))
            {
                roleDbSet.Add(Security.Authorization.Roles.ShowRemover.ToRoleEntity());
            }

            # endregion

            #region Award Roles definitios

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.AwardViewer)))
            {
                roleDbSet.Add(Security.Authorization.Roles.AwardViewer.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.AwardCreator)))
            {
                roleDbSet.Add(Security.Authorization.Roles.AwardCreator.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.AwardEditor)))
            {
                roleDbSet.Add(Security.Authorization.Roles.AwardEditor.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.AwardRemover)))
            {
                roleDbSet.Add(Security.Authorization.Roles.AwardRemover.ToRoleEntity());
            }

            #endregion

            #region Blog Roles definitios

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.BlogViewer)))
            {
                roleDbSet.Add(Security.Authorization.Roles.BlogViewer.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.BlogCreator)))
            {
                roleDbSet.Add(Security.Authorization.Roles.BlogCreator.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.BlogEditor)))
            {
                roleDbSet.Add(Security.Authorization.Roles.BlogEditor.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.BlogRemover)))
            {
                roleDbSet.Add(Security.Authorization.Roles.BlogRemover.ToRoleEntity());
            }

            #endregion

            #region ProductionTeam Roles definitios

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.ProductionTeamViewer)))
            {
                roleDbSet.Add(Security.Authorization.Roles.ProductionTeamViewer.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.ProductionTeamCreator)))
            {
                roleDbSet.Add(Security.Authorization.Roles.ProductionTeamCreator.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.ProductionTeamEditor)))
            {
                roleDbSet.Add(Security.Authorization.Roles.ProductionTeamEditor.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.ProductionTeamRemover)))
            {
                roleDbSet.Add(Security.Authorization.Roles.ProductionTeamRemover.ToRoleEntity());
            }

            #endregion

            #region Presentation Roles definitios

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.PresentationViewer)))
            {
                roleDbSet.Add(Security.Authorization.Roles.PresentationViewer.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.PresentationCreator)))
            {
                roleDbSet.Add(Security.Authorization.Roles.PresentationCreator.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.PresentationEditor)))
            {
                roleDbSet.Add(Security.Authorization.Roles.PresentationEditor.ToRoleEntity());
            }

            if (!roleDbSet.Any(role => role.Code.Equals(Security.Authorization.Roles.PresentationRemover)))
            {
                roleDbSet.Add(Security.Authorization.Roles.PresentationRemover.ToRoleEntity());
            }

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