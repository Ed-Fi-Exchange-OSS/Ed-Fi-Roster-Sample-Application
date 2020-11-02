using EdFi.Roster.Data;
using EdFi.Roster.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdFi.Roster.Explorer.Controllers
{
    public class LocalEducationAgenciesController : Controller
    {
        private readonly LocalEducationAgencyService localEducationAgencyService;

        public LocalEducationAgenciesController()
        {
            this.localEducationAgencyService = new LocalEducationAgencyService(new JsonFileDataService());
        }
        public IActionResult Index()
        {
            //Read any saved LEAs to be displayed
            return View(localEducationAgencyService.ReadAll());
        }

        public IActionResult LoadLeas()
        {
            var response = localEducationAgencyService.GetAllLocalEducationAgenciesWithExtendedInfoAsync().Result;
            localEducationAgencyService.Save(response.FullDataSet);
            ViewData["leaExtendedResponseInfo"] = response;
            return View("Index", response.FullDataSet);
        }
    }
}
