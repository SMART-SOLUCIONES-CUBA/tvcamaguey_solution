using Microsoft.AspNetCore.Http;
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
    public class PresentationsController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<Presentation, System.Guid, PresentationInputViewModel, PresentationDisplayViewModel>
    {
        private const string BILLBOARD_FILTER_SESSION_MAP_KEY = "billboard-filter-session";

        private readonly IEntityRepository<Show, System.Guid> showRepository;
        private readonly IEntityRepository<ProductionTeam, System.Guid> productionTeamRepository;

        public PresentationsController(IEntityRepository<ProductionTeam, System.Guid> productionTeamRepository, IEntityRepository<Show, System.Guid> showRepository, IEntityRepository<Presentation, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<PresentationsController> localizer, ILogger<PresentationsController> logger) : base(repository, localizer, logger)
        {
            this.showRepository = showRepository;
            this.productionTeamRepository = productionTeamRepository;
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.PresentationViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.PresentationCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.PresentationCreator)]
        public override IActionResult Create(PresentationInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.PresentationEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.PresentationEditor)]
        public override IActionResult Edit(PresentationInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.PresentationRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.PresentationRemover)]
        public override IActionResult Delete(PresentationDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PreCreate(Presentation entity, PresentationInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.Show = showRepository.Get(modelInput.ShowId);
            entity.ProductionTeam = productionTeamRepository.Get(modelInput.ProductionTeamId);
        }

        protected override void PreEdit(Presentation entity, PresentationInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (entity.Show.Id != modelInput.ShowId)
            {
                entity.Show = showRepository.Get(modelInput.ShowId);
            }

            if (entity.ProductionTeam.Id != modelInput.ProductionTeamId)
            {
                entity.ProductionTeam = productionTeamRepository.Get(modelInput.ProductionTeamId);
            }
        }

        protected override void ConfigureCreate(PresentationInputViewModel modelInput)
        {
            base.ConfigureCreate(modelInput);

            PopulateModelInputForAvailableShows(modelInput);
            PopulateModelInputForAvailableProductionTeams(modelInput);
        }

        protected override void ValidateCreate(PresentationInputViewModel modelInput)
        {
            base.ValidateCreate(modelInput);

            if (modelInput.ShowId == System.Guid.Empty)
            {
                ModelState.AddModelError("ShowId", "SelectionRequiredValidationMessage");
            }

            if (modelInput.ProductionTeamId == System.Guid.Empty)
            {
                ModelState.AddModelError("ProductionTeamId", "SelectionRequiredValidationMessage");
            }
        }

        protected override void ValidateEdit(PresentationInputViewModel modelInput)
        {
            base.ValidateEdit(modelInput);

            if (modelInput.ShowId == System.Guid.Empty)
            {
                ModelState.AddModelError("ShowId", "SelectionRequiredValidationMessage");
            }

            if (modelInput.ProductionTeamId == System.Guid.Empty)
            {
                ModelState.AddModelError("ProductionTeamId", "SelectionRequiredValidationMessage");
            }
        }

        protected override void ConfigureEdit(Presentation entity, PresentationInputViewModel modelInput)
        {
            base.ConfigureEdit(entity, modelInput);

            PopulateModelInputForAvailableShows(modelInput);
            PopulateModelInputForAvailableProductionTeams(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(PresentationInputViewModel modelInput)
        {
            base.ConfigurePostFailCreateValidation(modelInput);

            PopulateModelInputForAvailableShows(modelInput);
            PopulateModelInputForAvailableProductionTeams(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(PresentationInputViewModel modelInput)
        {
            base.ConfigurePostFailEditValidation(modelInput);

            PopulateModelInputForAvailableShows(modelInput);
            PopulateModelInputForAvailableProductionTeams(modelInput);
        }

        private void PopulateModelInputForAvailableShows(PresentationInputViewModel inputModel)
        {
            var entities = showRepository.GetAll().ToArray();

            foreach (var entity in entities)
            {
                inputModel.AvailableShowSelectListItems.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name });
            }
        }

        private void PopulateModelInputForAvailableProductionTeams(PresentationInputViewModel inputModel)
        {
            var entities = productionTeamRepository.GetAll().ToArray();

            foreach (var entity in entities)
            {
                inputModel.AvailableProductionTeamSelectListItems.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name });
            }
        }

        [HttpGet]
        [Route("[action]")]
        [RequiresRoleBasePermission(Security.Authorization.Roles.PresentationViewer)]
        public IActionResult Billboard()
        {
            var filter = new Models.Filter.BillboardFilterModel
            {
                StartDate = System.DateTime.Today,
                EndDate = System.DateTime.Today.AddDays(6)
            };

            try
            {
                filter = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Filter.BillboardFilterModel>(HttpContext.Session.GetString(BILLBOARD_FILTER_SESSION_MAP_KEY));
            }
            catch
            {                
            }

            var returnModel = new BillboardViewModel { Filter = filter };

            var currentDate = filter.StartDate;

            while (currentDate <= filter.EndDate)
            {
                var dailyBillboard = new DailyBillboardViewModel { At = currentDate, Presentations = Repository.FindBy(e => e.At.Date == currentDate).OrderBy(e => e.At).ToArray().Select((Presentation e) =>
                {
                    var model = new PresentationDisplayViewModel();

                    model.Import(e);

                    return model;
                }).ToList() };

                returnModel.DailyBillboards.Add(dailyBillboard);

                currentDate = currentDate.AddDays(1);
            }

            return View(returnModel);
        }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        [RequiresRoleBasePermission(Security.Authorization.Roles.PresentationViewer)]
        public IActionResult Billboard(Models.Filter.BillboardFilterModel model)
        {
            if (model.StartDate > model.EndDate)
            {
                ModelState.AddModelError("StartDate", localizer["DateTimeRangeInvalidMessage"]);
            }

            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString(BILLBOARD_FILTER_SESSION_MAP_KEY, Newtonsoft.Json.JsonConvert.SerializeObject(model));
            }
            else
            {
                var notification = new SAPRESSC.Core.Common.Models.View.NotificationViewModel
                {
                    Type = SAPRESSC.Core.Common.Models.View.NotificationType.Danger,
                    Message = GetInvalidMessage(),
                    Messages = new System.Collections.Generic.List<string>()
                };

                foreach (var item in ModelState)
                {
                    if (ModelState.GetFieldValidationState(item.Key) != Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid) continue;

                    foreach (var error in item.Value.Errors)
                    {
                        notification.Messages.Add(error.ErrorMessage);
                    }
                }

                TempData[SAPRESSC.Core.Common.Models.View.NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);
            }

            return RedirectToAction("billboard");
        }

        protected override IActionResult GetDefaultActionResult()
        {
            return Redirect("billboard");
        }
    }
}
