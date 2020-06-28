using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models;
using SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.Entities;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Controllers.Api
{
    public class PresentationsController : SAPRESSC.Core.Web.Api.Controllers.AbstractEntityApiManagementFilteredController<Presentation, System.Guid, PresentationInputModel, PresentationDisplayModel, Internet.Api.Common.Models.Filter.PresentationFilterModel>
    {
        public PresentationsController(IEntityRepository<Presentation, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<PresentationsController> localizer, ILogger<PresentationsController> logger) : base(repository, localizer, logger)
        {
        }

        public override IActionResult GetPaginate(int sheet = 1, int limit = 25)
        {
            return new NotFoundResult();
        }

        public override IActionResult Create([FromBody] PresentationInputModel inputModel)
        {
            return new NotFoundResult();
        }

        public override IActionResult Edit([FromBody] PresentationInputModel inputModel)
        {
            return new NotFoundResult();
        }

        public override IActionResult Delete(System.Guid key)
        {
            return new NotFoundResult();
        }
    }
}
