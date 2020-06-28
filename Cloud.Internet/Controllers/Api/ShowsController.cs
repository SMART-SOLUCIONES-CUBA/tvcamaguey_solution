using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models;
using SmartSolucionesCuba.TVCamaguey.Cloud.Persistence.Entities;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Controllers.Api
{
    [Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class ShowsController : SAPRESSC.Core.Web.Api.Controllers.AbstractEntityApiManagementController<Show, System.Guid, ShowInputModel, ShowDisplayModel>
    {
        public ShowsController(IEntityRepository<Show, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ShowsController> localizer, ILogger<ShowsController> logger) : base(repository, localizer, logger)
        {
        }

        public override IActionResult Create([FromBody] ShowInputModel inputModel)
        {
            return new NotFoundResult();
        }

        public override IActionResult Edit([FromBody] ShowInputModel inputModel)
        {
            return new NotFoundResult();
        }

        public override IActionResult Delete(System.Guid key)
        {
            return new NotFoundResult();
        }
    }
}
