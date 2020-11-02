using EdFi.Roster.Sdk.Models;
using EdFi.Roster.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EdFi.Roster.Explorer.Controllers
{
    public class EdFiApiSettings : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var service = new ApiSettingsService();
            var model = service.Read();
            return View(model);
        }

        [HttpPost]
        public IActionResult SaveSettings(string rootUrl, string key, string secret)
        {
            //save the settings
            var service = new ApiSettingsService();
            var model = new ApiSettings{Key = key, RootUrl = rootUrl, Secret = secret};
            service.Save(model);
            return new JsonResult(model);
        }

        [HttpPost]
        public IActionResult TestConnection(string rootUrl, string key, string secret)
        {
            //Get token info to test the connection
            var service = new BearerTokenService();
            var response = service.GetNewBearerTokenResponse(rootUrl, key, secret);
            var content = JsonConvert.SerializeObject(response);
            return Content(content, "application/json");
        }
    }
}
