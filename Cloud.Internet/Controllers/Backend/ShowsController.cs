using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View;
using SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.Entities;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequiresRoleBasePermission(SAPRESSC.Core.Web.Security.SecurityRoleCodes.ShellManager)]
    [Area("business")]
    public class ShowsController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<Show, System.Guid, ShowInputViewModel, ShowDisplayViewModel>
    {
        public ShowsController(IEntityRepository<Show, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ShowsController> localizer, ILogger<ShowsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.ShowViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        public override IActionResult Create()
        {
            return NotFound();
        }

        public override IActionResult Create(ShowInputViewModel modelInput)
        {
            return NotFound();
        }

        public override IActionResult Edit(System.Guid key)
        {
            return NotFound();
        }

        public override IActionResult Edit(ShowInputViewModel modelInput)
        {
            return NotFound();
        }

        public override IActionResult Delete(System.Guid key)
        {
            return NotFound();
        }

        public override IActionResult Delete(ShowDisplayViewModel displayModel)
        {
            return NotFound();
        }
    }
}
