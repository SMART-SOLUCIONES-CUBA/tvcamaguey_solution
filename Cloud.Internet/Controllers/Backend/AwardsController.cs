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
    public class AwardsController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<Award, System.Guid, AwardInputViewModel, AwardDisplayViewModel>
    {
        private readonly IEntityRepository<SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity, System.Guid> identityRepository;

        public AwardsController(IEntityRepository<SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity, System.Guid> identityRepository, IEntityRepository<Award, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<AwardsController> localizer, ILogger<AwardsController> logger) : base(repository, localizer, logger)
        {
            this.identityRepository = identityRepository;
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.AwardViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        public override IActionResult Create()
        {
            return NotFound();
        }

        public override IActionResult Create(AwardInputViewModel modelInput)
        {
            return NotFound();
        }

        public override IActionResult Edit(System.Guid key)
        {
            return NotFound();
        }

        public override IActionResult Edit(AwardInputViewModel modelInput)
        {
            return NotFound();
        }

        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        public override IActionResult Delete(AwardDisplayViewModel displayModel)
        {
            return NotFound();
        }

        #region Award Awardeds

        [Route("edit/{key}/awardeds")]
        [RequiresRoleBasePermission(Security.Authorization.Roles.AwardViewer)]
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

                var resultModel = new AwardAwardedsViewModel
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
