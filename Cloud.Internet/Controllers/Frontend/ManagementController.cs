namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Controllers.Frontend
{
    public class ManagementController : Microsoft.AspNetCore.Mvc.Controller
    {
        public Microsoft.AspNetCore.Mvc.IActionResult Index()
        {
            return RedirectToAction("index", "dashboard");
        }
    }
}