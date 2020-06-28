using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Controllers.Api
{
    [Route("api/[controller]")]
    public class MediaFileUploadController : SAPRESSC.Core.Web.Common.Controllers.BaseWebController
    {
        private readonly SAPRESSC.Middleware.Media.Web.Common.Managers.Facade.IImageFileManagerFacade imageFileManager;

        public MediaFileUploadController(SAPRESSC.Middleware.Media.Web.Common.Managers.Facade.IImageFileManagerFacade imageFileManager, Microsoft.Extensions.Localization.IStringLocalizer<MediaFileUploadController> localizer, ILogger<MediaFileUploadController> logger) : base(localizer, logger)
        {
            this.imageFileManager = imageFileManager;
        }

        [HttpPost("image")]
        public virtual IActionResult Image([FromBody] Internet.Api.Common.Models.MediaFileUploadModel model)
        {
            try
            {
                imageFileManager.SaveImageFileContent(model.Id, model.ContentData, model.ContentType);

                return Ok();
            }
            catch (System.Exception e)
            {
                logger.LogError(e, e.Message, null);

                return BadRequest();
            }            
        }
    }
}
