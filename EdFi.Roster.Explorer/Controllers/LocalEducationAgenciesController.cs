using System.Threading.Tasks;
using EdFi.Roster.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdFi.Roster.Explorer.Controllers
{
    public class LocalEducationAgenciesController : Controller
    {
        private readonly LocalEducationAgencyService _localEducationAgencyService;

        public LocalEducationAgenciesController(LocalEducationAgencyService localEducationAgencyService)
        {
            _localEducationAgencyService = localEducationAgencyService;
        }
        public async Task<IActionResult> Index()
        {
            //Read any saved LEAs to be displayed
            return View(await _localEducationAgencyService.ReadAllAsync());
        }

        public async Task<IActionResult> LoadLeas()
        {
            var response = await _localEducationAgencyService.GetAllLocalEducationAgenciesWithExtendedInfoAsync();
            await _localEducationAgencyService.Save(response.FullDataSet);
            ViewData["leaExtendedResponseInfo"] = response;
            return View("Index", response.FullDataSet);
        }
    }
}
