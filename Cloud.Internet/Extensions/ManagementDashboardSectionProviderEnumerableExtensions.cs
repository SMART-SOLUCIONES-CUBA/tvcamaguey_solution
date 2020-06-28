using SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Models.View;
using System.Collections.Generic;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Extensions
{
    public static class ManagementDashboardSectionProviderEnumerableExtensions
    {
        public static Dictionary<string, ICollection<ManagementDashboardSectionViewModel>> ToManagementDashboardSectionGroups(this IEnumerable<SAPRESSC.Core.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade> self)
        {
            var securitySections = new List<ManagementDashboardSectionViewModel>();
            var businessSections = new List<ManagementDashboardSectionViewModel>();
            var communicationSections = new List<ManagementDashboardSectionViewModel>();
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
                Title = "InstitutionInfo",
                Controller = "institutioninfo",
                Action = "index",
                Area = string.Empty
            });
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

            communicationSections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Posts",
                Controller = "posts",
                Action = "index",
                Area = "communication",
                Role = Security.Authorization.Roles.PostViewer
            });
            communicationSections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Categories",
                Controller = "categories",
                Action = "index",
                Area = "communication",
                Role = Security.Authorization.Roles.CategoryViewer
            });
            communicationSections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Tags",
                Controller = "tags",
                Action = "index",
                Area = "communication",
                Role = Security.Authorization.Roles.TagViewer
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
                { "Comunicación", communicationSections },
                { "Extensión", extensionSections }
            };
        }
    }
}
