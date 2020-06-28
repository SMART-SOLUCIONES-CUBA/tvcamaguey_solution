using SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Models.View;
using System.Collections.Generic;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Extensions
{
    public static class ManagementDashboardSectionProviderEnumerableExtensions
    {
        public static Dictionary<string, ICollection<ManagementDashboardSectionViewModel>> ToManagementDashboardSectionGroups(this IEnumerable<SAPRESSC.Core.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade> self)
        {
            var securitySections = new List<ManagementDashboardSectionViewModel>();
            var businessSections = new List<ManagementDashboardSectionViewModel>();
            var extensionSections = new List<ManagementDashboardSectionViewModel>();

            foreach (var provider in self)
            {
                if (provider is SAPRESSC.Core.Web.SecurityLocal.Providers.LocalSecurityWebManagementDashboardSectionProvider)
                {
                    securitySections.AddRange(provider.GetManagementDashboardSections());
                }
            }

            businessSections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Employees",
                Controller = "employees",
                Action = "index",
                Area = "business",
                Role = Security.Authorization.Roles.EmployViewer
            });
            businessSections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Awards",
                Controller = "awards",
                Action = "index",
                Area = "business",
                Role = Security.Authorization.Roles.AwardViewer
            });
            businessSections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Shows",
                Controller = "shows",
                Action = "index",
                Area = "business",
                Role = Security.Authorization.Roles.ShowViewer
            });
            businessSections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "ProductionTeams",
                Controller = "productionteams",
                Action = "index",
                Area = "business",
                Role = Security.Authorization.Roles.ProductionTeamViewer
            });
            businessSections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Presentations",
                Controller = "presentations",
                Action = "billboard",
                Area = "business",
                Role = Security.Authorization.Roles.PresentationViewer
            });

            extensionSections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Blogs",
                Controller = "blogs",
                Action = "index",
                Area = "extension",
                Role = Security.Authorization.Roles.BlogViewer
            });

            return new Dictionary<string, ICollection<ManagementDashboardSectionViewModel>>
            {
                { "Seguridad", securitySections },
                { "Funcionamiento", businessSections },
                { "Extensión", extensionSections }
            };
        }
    }
}
