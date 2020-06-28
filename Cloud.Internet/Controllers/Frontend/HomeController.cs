using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Controllers.Frontend
{
    [ChangeUserPasswordRequiredAction]
    [CatchChangeUserPasswordRequiredException]
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        public Microsoft.AspNetCore.Mvc.IActionResult Index()
        {
            return RedirectToAction("index", "dashboard");
        }
    }
}
