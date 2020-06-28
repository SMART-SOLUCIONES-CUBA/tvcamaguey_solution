using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Api.Common.Models;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Controllers.Api
{
    public class PostsController : SAPRESSC.Core.Web.Api.Controllers.AbstractEntityApiManagementFilteredController<Persistence.Entities.Post, string, PostInputModel, PostDisplayModel, Internet.Api.Common.Models.Filter.PostFilterModel>
    {
        public PostsController(SAPRESSC.Core.Persistence.Repositories.IEntityRepository<Persistence.Entities.Post, string> repository, Microsoft.Extensions.Localization.IStringLocalizer<PostsController> localizer, ILogger<PostsController> logger) : base(repository, localizer, logger)
        {
        }

        public override IActionResult Create([FromBody] PostInputModel inputModel)
        {
            return new NotFoundResult();
        }

        public override IActionResult Edit([FromBody] PostInputModel inputModel)
        {
            return new NotFoundResult();
        }

        public override IActionResult Delete(string key)
        {
            return new NotFoundResult();
        }
    }
}
