using Microsoft.AspNetCore.Http;
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
    public class PresentationsController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<Presentation, System.Guid, PresentationInputViewModel, PresentationDisplayViewModel>
    {
        private const string BILLBOARD_FILTER_SESSION_MAP_KEY = "billboard-filter-session";

        public PresentationsController(IEntityRepository<Presentation, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<PresentationsController> localizer, ILogger<PresentationsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.PresentationViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        public override IActionResult Create()
        {
            return NotFound();
        }

        public override IActionResult Create(PresentationInputViewModel modelInput)
        {
            return NotFound();
        }

        public override IActionResult Edit(System.Guid key)
        {
            return NotFound();
        }

        public override IActionResult Edit(PresentationInputViewModel modelInput)
        {
            return NotFound();
        }

        public override IActionResult Delete(System.Guid key)
        {
            return NotFound();
        }

        public override IActionResult Delete(PresentationDisplayViewModel displayModel)
        {
            return NotFound();
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
