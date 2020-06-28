namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Controllers.Frontend
{
    public class ErrorController : Microsoft.AspNetCore.Mvc.Controller
    {
        [Microsoft.AspNetCore.Mvc.ResponseCache(Duration = 0, Location = Microsoft.AspNetCore.Mvc.ResponseCacheLocation.None, NoStore = true)]
        public Microsoft.AspNetCore.Mvc.IActionResult Index()
        {
            return View(new Models.View.ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
