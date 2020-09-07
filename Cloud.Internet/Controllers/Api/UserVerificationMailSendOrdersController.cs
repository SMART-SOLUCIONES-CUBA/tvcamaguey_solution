using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Mailer.Api.Common.Models;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Mailer.Persistence.Entities;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Controllers.Api
{
    // TODO Add security layer attribute(s)
    public class UserVerificationMailSendOrdersController : SAPRESSC.Core.Web.Api.Controllers.AbstractEntityApiManagementController<UserVerificationMailSendOrder, System.Guid, UserVerificationMailSendOrderInputModel, UserVerificationMailSendOrderDisplayModel>
    {
        public UserVerificationMailSendOrdersController(SAPRESSC.Core.Persistence.Repositories.IEntityRepository<UserVerificationMailSendOrder, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<UserVerificationMailSendOrdersController> localizer, ILogger<UserVerificationMailSendOrdersController> logger) : base(repository, localizer, logger)
        {
        }

        public override IActionResult Create([FromBody] UserVerificationMailSendOrderInputModel inputModel)
        {
            return new NotFoundResult();
        }

        public override IActionResult Edit([FromBody] UserVerificationMailSendOrderInputModel inputModel)
        {
            return new NotFoundResult();
        }
    }
}