using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Controllers.Api
{
    [Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class PersonIdentitiesController : SAPRESSC.Core.Web.Api.Controllers.AbstractEntityApiManagementController<PersonIdentity, System.Guid, PersonIdentityInputModel, PersonIdentityDisplayModel>
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<Persistence.Entities.TerminalUser> userManager;

        public PersonIdentitiesController(Microsoft.AspNetCore.Identity.UserManager<Persistence.Entities.TerminalUser> userManager, IEntityRepository<PersonIdentity, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<PersonIdentitiesController> localizer, ILogger<PersonIdentitiesController> logger) : base(repository, localizer, logger)
        {
            this.userManager = userManager;
        }

        [HttpGet("self")]
        public async System.Threading.Tasks.Task<IActionResult> SelfAsync()
        {
            var user = await userManager.GetUserAsync(User);

            var entity = Repository.Get(user.IdentityId);

            var model = new PersonIdentityDisplayModel();

            model.Import(entity);

            return Ok(model);
        }
    }
}
