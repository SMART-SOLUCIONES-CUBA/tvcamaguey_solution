using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Models.View;
using SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.Entities;
using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequiresRoleBasePermission(SAPRESSC.Core.Web.Security.SecurityRoleCodes.ShellManager)]
    [Area("business")]
    public class AwardsController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<Award, System.Guid, AwardInputViewModel, AwardDisplayViewModel>
    {
        private readonly IEntityRepository<Employ, System.Guid> employRepository;
        private readonly IEntityRepository<SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity, System.Guid> identityRepository;

        public AwardsController(IEntityRepository<SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity, System.Guid> identityRepository, IEntityRepository<Employ, System.Guid> employRepository, IEntityRepository<Award, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<AwardsController> localizer, ILogger<AwardsController> logger) : base(repository, localizer, logger)
        {
            this.employRepository = employRepository;
            this.identityRepository = identityRepository;
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.AwardViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.AwardCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.AwardCreator)]
        public override IActionResult Create(AwardInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.AwardEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.AwardEditor)]
        public override IActionResult Edit(AwardInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.AwardRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.AwardRemover)]
        public override IActionResult Delete(AwardDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        #region Award Awardeds Management

        [Route("edit/{key}/awardeds")]
        [RequiresRoleBasePermission(Security.Authorization.Roles.AwardEditor)]
        public IActionResult Awardeds(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var awardeds = entity.EmployBridges.Select(b => b.Employ).ToArray();

                foreach (var awarded in awardeds)
                {
                    awarded.Identity = identityRepository.Get(awarded.IdentityId);
                }

                var resultModel = new AwardAwardedsManageViewModel
                {
                    Award = new AwardDisplayViewModel(),
                    Awardeds = awardeds.Select((Employ employ) =>
                    {
                        var model = new EmployDisplayViewModel();

                        model.Import(employ);

                        return model;
                    }).ToList()
                };

                resultModel.Award.Import(entity);

                resultModel.AvailableEmploys = employRepository.GetAll().ToArray().Except(awardeds, new Helpers.Comparing.EmployComparingHelper()).Select(e => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
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

            SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [Route("edit/awardeds/add")]
        [RequiresRoleBasePermission(Security.Authorization.Roles.AwardEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAwarded(AwardEmployBridgeInputViewModel modelInput)
        {
            Award award = null;

            try
            {
                award = Repository.Get(modelInput.AwardId);
            }
            catch { }

            if (award == null)
            {
                SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (modelInput.EmployId == System.Guid.Empty)
            {
                ModelState.AddModelError("EmployId", localizer["EmploySelectionRequiredMessage"]);
            }

            if (ModelState.IsValid)
            {
                award.EmployBridges.Add(new EmployAwardBridge { AwardId = award.Id, EmployId = modelInput.EmployId });

                Repository.Update(award, User.Identity.Name);

                SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Success, GetCreationSuccessMessage());
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

                SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Danger, messages.ToArray());
            }

            return RedirectToAction("awardeds", new { key = award.Id });
        }

        [Route("edit/awardeds/delete")]
        [RequiresRoleBasePermission(Security.Authorization.Roles.AwardEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAwarded(AwardEmployBridgeDisplayViewModel modelDisplay)
        {
            Award award = null;

            try
            {
                award = Repository.Get(modelDisplay.AwardId);
            }
            catch { }

            if (award == null)
            {
                SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            try
            {
                var bridge = award.EmployBridges.First(b => b.EmployId == modelDisplay.EmployId);

                award.EmployBridges.Remove(bridge);

                Repository.Update(award, User.Identity.Name);

                SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Success, GetDeletionSuccessMessage());
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            return RedirectToAction("awardeds", new { key = award.Id });
        }

        #endregion
    }
}
