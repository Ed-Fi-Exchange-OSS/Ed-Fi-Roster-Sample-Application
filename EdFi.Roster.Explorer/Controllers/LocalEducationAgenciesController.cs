using System.Threading.Tasks;
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
        public async Task<IActionResult> Index()
        {
            //Read any saved LEAs to be displayed
            return View(await localEducationAgencyService.ReadAll());
        }

        public async Task<IActionResult> LoadLeas()
        {
            var response = await localEducationAgencyService.GetAllLocalEducationAgenciesWithExtendedInfoAsync();
            localEducationAgencyService.Save(response.FullDataSet);
            ViewData["leaExtendedResponseInfo"] = response;
            return View("Index", response.FullDataSet);
        }
    }
}
