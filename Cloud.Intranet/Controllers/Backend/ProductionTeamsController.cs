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
    public class ProductionTeamsController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<ProductionTeam, System.Guid, ProductionTeamInputViewModel, ProductionTeamDisplayViewModel>
    {
        private readonly IEntityRepository<Employ, System.Guid> employRepository;
        private readonly IEntityRepository<SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity, System.Guid> identityRepository;

        public ProductionTeamsController(IEntityRepository<SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity, System.Guid> identityRepository, IEntityRepository<Employ, System.Guid> employRepository, IEntityRepository<ProductionTeam, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ProductionTeamsController> localizer, ILogger<ProductionTeamsController> logger) : base(repository, localizer, logger)
        {
            this.employRepository = employRepository;
            this.identityRepository = identityRepository;
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.ProductionTeamViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.ProductionTeamCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.ProductionTeamCreator)]
        public override IActionResult Create(ProductionTeamInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.ProductionTeamEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.ProductionTeamEditor)]
        public override IActionResult Edit(ProductionTeamInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [HttpGet]
        [Route("[action]")]
        [RequiresRoleBasePermission(Security.Authorization.Roles.ProductionTeamEditor)]
        public IActionResult Duplicate(System.Guid key)
        {
            return base.Edit(key);
        }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        [RequiresRoleBasePermission(Security.Authorization.Roles.ProductionTeamCreator)]
        public IActionResult Duplicate(ProductionTeamInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.ProductionTeamRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.ProductionTeamRemover)]
        public override IActionResult Delete(ProductionTeamDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void ValidateCreate(ProductionTeamInputViewModel modelInput)
        {
            base.ValidateCreate(modelInput);

            if (Repository.FindOneBy(entity => entity.Name == modelInput.Name) != null)
            {
                ModelState.AddModelError("Name", "El valor especificado en el campo Nombre ya está en uso. Este valor debe ser único para cada Equipo de Realización.");
            }
        }

        protected override void PreCreate(ProductionTeam entity, ProductionTeamInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            if (modelInput.Id != System.Guid.Empty)
            {
                var duplicationTargetEntity = Repository.Get(modelInput.Id);

                foreach (var item in duplicationTargetEntity.EmployBridges)
                {
                    entity.EmployBridges.Add(new ProductionTeamEmployBridge { ProductionTeamId = entity.Id, EmployId = item.EmployId, ProductionRole = item.ProductionRole });
                }
            }
        }

        #region ProductionTeam Members Management

        [Route("edit/{key}/members")]
        [RequiresRoleBasePermission(Security.Authorization.Roles.ProductionTeamEditor)]
        public IActionResult Members(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var resultModel = new ProductionTeamMembersManageViewModel
                {
                    ProductionTeam = new ProductionTeamDisplayViewModel(),
                    Members = entity.EmployBridges.OrderBy(b => b.ProductionRole).Select((ProductionTeamEmployBridge bridge) =>
                    {
                        var model = new ProductionTeamEmployBridgeDisplayViewModel
                        {
                            EmployId = bridge.EmployId,
                            Employ = new EmployDisplayViewModel(),
                            ProductionTeamId = entity.Id,
                            ProductionTeam = new ProductionTeamDisplayViewModel(),
                            ProductionRole = bridge.ProductionRole
                        };

                        bridge.Employ.Identity = identityRepository.Get(bridge.Employ.IdentityId);

                        model.ProductionTeam.Import(entity);
                        model.Employ.Import(bridge.Employ);

                        return model;
                    }).ToList()
                };

                resultModel.ProductionTeam.Import(entity);

                resultModel.AvailableEmploys = employRepository.GetAll().ToArray().Select(e => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
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

        [Route("edit/members/add")]
        [RequiresRoleBasePermission(Security.Authorization.Roles.ProductionTeamEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMember(ProductionTeamEmployBridgeInputViewModel modelInput)
        {
            ProductionTeam productionTeam = null;

            try
            {
                productionTeam = Repository.Get(modelInput.ProductionTeamId);
            }
            catch { }

            if (productionTeam == null)
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
                productionTeam.EmployBridges.Add(new ProductionTeamEmployBridge { ProductionTeamId = productionTeam.Id, EmployId = modelInput.EmployId, ProductionRole = modelInput.ProductionRole });

                Repository.Update(productionTeam, User.Identity.Name);

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

            return RedirectToAction("members", new { key = productionTeam.Id });
        }

        [Route("edit/members/delete")]
        [RequiresRoleBasePermission(Security.Authorization.Roles.ProductionTeamEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteMember(ProductionTeamEmployBridgeDisplayViewModel modelDisplay)
        {
            ProductionTeam productionTeam = null;

            try
            {
                productionTeam = Repository.Get(modelDisplay.ProductionTeamId);
            }
            catch { }

            if (productionTeam == null)
            {
                SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            try
            {
                var bridge = productionTeam.EmployBridges.First(b => b.EmployId == modelDisplay.EmployId);

                productionTeam.EmployBridges.Remove(bridge);

                Repository.Update(productionTeam, User.Identity.Name);

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

            return RedirectToAction("members", new { key = productionTeam.Id });
        }

        #endregion
    }
}
