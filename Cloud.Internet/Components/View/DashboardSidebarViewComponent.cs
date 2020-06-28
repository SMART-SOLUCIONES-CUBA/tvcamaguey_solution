using Microsoft.AspNetCore.Mvc;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Extensions;
using System.Collections.Generic;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Components.View
{
    public class DashboardSidebarViewComponent : ViewComponent
    {
        private readonly IEnumerable<SAPRESSC.Core.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade> managementDashboardSectionProviders;

        public DashboardSidebarViewComponent(IEnumerable<SAPRESSC.Core.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade> managementDashboardSectionProviders)
        {
            this.managementDashboardSectionProviders = managementDashboardSectionProviders;
        }

        public IViewComponentResult Invoke()
        {
            return View(managementDashboardSectionProviders.ToManagementDashboardSectionGroups());
        }
    }
}
