using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.Entities;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequiresRoleBasePermission(SAPRESSC.Core.Web.Security.SecurityRoleCodes.ShellManager)]
    [Area("communication")]
    public class TagsController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<Tag, string, TagInputViewModel, TagDisplayViewModel>
    {
        public TagsController(SAPRESSC.Core.Persistence.Repositories.IEntityRepository<Tag, string> repository, Microsoft.Extensions.Localization.IStringLocalizer<TagsController> localizer, ILogger<TagsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.TagViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.TagCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.TagCreator)]
        public override IActionResult Create(TagInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.TagEditor)]
        public override IActionResult Edit(string key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.TagEditor)]
        public override IActionResult Edit(TagInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.TagRemover)]
        public override IActionResult Delete(string key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.TagRemover)]
        public override IActionResult Delete(TagDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
