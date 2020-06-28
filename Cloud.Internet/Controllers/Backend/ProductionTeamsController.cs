using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Models.View;
using SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.Entities;
using System.Linq;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequiresRoleBasePermission(SAPRESSC.Core.Web.Security.SecurityRoleCodes.ShellManager)]
    [Area("business")]
    public class ProductionTeamsController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<ProductionTeam, System.Guid, ProductionTeamInputViewModel, ProductionTeamDisplayViewModel>
    {
        private readonly IEntityRepository<SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity, System.Guid> identityRepository;

        public ProductionTeamsController(IEntityRepository<SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity, System.Guid> identityRepository, IEntityRepository<ProductionTeam, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ProductionTeamsController> localizer, ILogger<ProductionTeamsController> logger) : base(repository, localizer, logger)
        {
            this.identityRepository = identityRepository;
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.ProductionTeamViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        public override IActionResult Create()
        {
            return NotFound();
        }

        public override IActionResult Create(ProductionTeamInputViewModel modelInput)
        {
            return NotFound();
        }

        public override IActionResult Edit(System.Guid key)
        {
            return NotFound();
        }

        public override IActionResult Edit(ProductionTeamInputViewModel modelInput)
        {
            return NotFound();
        }

        public override IActionResult Delete(System.Guid key)
        {
            return NotFound();
        }

        public override IActionResult Delete(ProductionTeamDisplayViewModel displayModel)
        {
            return NotFound();
        }

        #region ProductionTeam Members

        [Route("edit/{key}/members")]
        [RequiresRoleBasePermission(Security.Authorization.Roles.ProductionTeamViewer)]
        public IActionResult Members(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var resultModel = new ProductionTeamMembersViewModel
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

                return View(resultModel);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        #endregion
    }
}
