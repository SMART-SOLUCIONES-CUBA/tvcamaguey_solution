using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Controllers.Api
{
    public class EmployeesController : SAPRESSC.Core.Web.Api.Controllers.AbstractEntityApiManagementController<Cloud.Persistence.Entities.Employ, System.Guid, EmployInputModel, EmployDisplayModel>
    {
        public EmployeesController(SAPRESSC.Core.Persistence.Repositories.IEntityRepository<Cloud.Persistence.Entities.Employ, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<EmployeesController> localizer, ILogger<EmployeesController> logger) : base(repository, localizer, logger)
        {
        }

        public override IActionResult GetPaginate(int sheet = 1, int limit = 25)
        {
            return new NotFoundResult();
        }

        public override IActionResult Create([FromBody] EmployInputModel inputModel)
        {
            return new NotFoundResult();
        }

        public override IActionResult Edit([FromBody] EmployInputModel inputModel)
        {
            return new NotFoundResult();
        }

        public override IActionResult Delete(System.Guid key)
        {
            return new NotFoundResult();
        }
    }
}
