using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View;
using SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.Entities;
using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequiresRoleBasePermission(SAPRESSC.Core.Web.Security.SecurityRoleCodes.ShellManager)]
    [Area("extension")]
    public class BlogsController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<Blog, System.Guid, BlogInputViewModel, BlogDisplayViewModel>
    {
        private readonly IEntityRepository<SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity, System.Guid> identityRepository;

        public BlogsController(IEntityRepository<SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity, System.Guid> identityRepository, IEntityRepository<Blog, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<BlogsController> localizer, ILogger<BlogsController> logger) : base(repository, localizer, logger)
        {
            this.identityRepository = identityRepository;
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.BlogViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        public override IActionResult Create()
        {
            return NotFound();
        }

        public override IActionResult Create(BlogInputViewModel modelInput)
        {
            return NotFound();
        }

        public override IActionResult Edit(System.Guid key)
        {
            return NotFound();
        }

        public override IActionResult Edit(BlogInputViewModel modelInput)
        {
            return NotFound();
        }

        public override IActionResult Delete(System.Guid key)
        {
            return NotFound();
        }

        public override IActionResult Delete(BlogDisplayViewModel displayModel)
        {
            return NotFound();
        }

        #region Blog Authors

        [Route("edit/{key}/authors")]
        [RequiresRoleBasePermission(Security.Authorization.Roles.BlogViewer)]
        public IActionResult Authors(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var authors = entity.EmployBridges.Select(b => b.Employ).ToArray();

                foreach (var author in authors)
                {
                    author.Identity = identityRepository.Get(author.IdentityId);
                }

                var resultModel = new BlogAutorsViewModel
                {
                    Blog = new BlogDisplayViewModel(),
                    Authors = authors.Select((Employ employ) =>
                    {
                        var model = new EmployDisplayViewModel();

                        model.Import(employ);

                        return model;
                    }).ToList()
                };

                resultModel.Blog.Import(entity);

                return View(resultModel);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        #endregion
    }
}
