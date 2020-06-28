using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Models.View;
using SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.Entities;
using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequiresRoleBasePermission(SAPRESSC.Core.Web.Security.SecurityRoleCodes.ShellManager)]
    [Area("extension")]
    public class BlogsController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<Blog, System.Guid, BlogInputViewModel, BlogDisplayViewModel>
    {
        private readonly IEntityRepository<Employ, System.Guid> employRepository;
        private readonly IEntityRepository<SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity, System.Guid> identityRepository;

        public BlogsController(IEntityRepository<SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity, System.Guid> identityRepository, IEntityRepository<Employ, System.Guid> employRepository, IEntityRepository<Blog, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<BlogsController> localizer, ILogger<BlogsController> logger) : base(repository, localizer, logger)
        {
            this.employRepository = employRepository;
            this.identityRepository = identityRepository;
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.BlogViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.BlogCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.BlogCreator)]
        public override IActionResult Create(BlogInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.BlogEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.BlogEditor)]
        public override IActionResult Edit(BlogInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.BlogRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.BlogRemover)]
        public override IActionResult Delete(BlogDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        #region Blog Authors Management

        [Route("edit/{key}/authors")]
        [RequiresRoleBasePermission(Security.Authorization.Roles.BlogEditor)]
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

                var resultModel = new BlogAutorsManageViewModel
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

                resultModel.AvailableEmploys = employRepository.GetAll().ToArray().Except(authors, new Helpers.Comparing.EmployComparingHelper()).Select(e => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = string.Format("{0} {1}", e.Identity.Name, e.Identity.LastName)
                }).ToList();

                return View(resultModel);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [Route("edit/authors/add")]
        [RequiresRoleBasePermission(Security.Authorization.Roles.BlogEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAuthor(BlogEmployBridgeInputViewModel modelInput)
        {
            Blog blog = null;

            try
            {
                blog = Repository.Get(modelInput.BlogId);
            }
            catch { }

            if (blog == null)
            {
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (modelInput.EmployId == System.Guid.Empty)
            {
                ModelState.AddModelError("EmployId", localizer["EmploySelectionRequiredMessage"]);
            }

            if (ModelState.IsValid)
            {
                blog.EmployBridges.Add(new EmployBlogBridge { BlogId = blog.Id, EmployId = modelInput.EmployId });

                Repository.Update(blog, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());
            }
            else
            {
                var messages = new System.Collections.Generic.List<string>();

                foreach (var item in ModelState)
                {
                    if (ModelState.GetFieldValidationState(item.Key) != Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid) continue;

                    foreach (var error in item.Value.Errors)
                    {
                        messages.Add(error.ErrorMessage);
                    }
                }

                SetDefaultNotification(NotificationType.Danger, messages.ToArray());
            }

            return RedirectToAction("authors", new { key = blog.Id });
        }

        [Route("edit/albums/delete")]
        [RequiresRoleBasePermission(Security.Authorization.Roles.BlogEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAuthor(BlogEmployBridgeDisplayViewModel modelDisplay)
        {
            Blog blog = null;

            try
            {
                blog = Repository.Get(modelDisplay.BlogId);
            }
            catch { }

            if (blog == null)
            {
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            try
            {
                var bridge = blog.EmployBridges.First(b => b.EmployId == modelDisplay.EmployId);

                blog.EmployBridges.Remove(bridge);

                Repository.Update(blog, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            return RedirectToAction("authors", new { key = blog.Id });
        }

        #endregion
    }
}
