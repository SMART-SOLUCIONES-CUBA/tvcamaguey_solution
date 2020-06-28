using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.Entities;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Persistence.Enumerators;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Controllers.Api
{
    public class InstitutionInfoNomenclatorsController : SAPRESSC.Core.Web.Api.Controllers.AbstractEntityApiManagementController<InstitutionInfoNomenclator, InstitutionInfoKind, InstitutionInfoNomenclatorInputModel, InstitutionInfoNomenclatorDisplayModel>
    {
        public InstitutionInfoNomenclatorsController(SAPRESSC.Core.Persistence.Repositories.IEntityRepository<InstitutionInfoNomenclator, InstitutionInfoKind> repository, Microsoft.Extensions.Localization.IStringLocalizer<InstitutionInfoNomenclatorsController> localizer, ILogger<InstitutionInfoNomenclatorsController> logger) : base(repository, localizer, logger)
        {
        }

        public override IActionResult GetPaginate(int sheet = 1, int limit = 25)
        {
            return new NotFoundResult();
        }

        public override IActionResult Create([FromBody] InstitutionInfoNomenclatorInputModel inputModel)
        {
            return new NotFoundResult();
        }

        public override IActionResult Edit([FromBody] InstitutionInfoNomenclatorInputModel inputModel)
        {
            return new NotFoundResult();
        }

        public override IActionResult Delete(InstitutionInfoKind key)
        {
            return new NotFoundResult();
        }
    }
}
