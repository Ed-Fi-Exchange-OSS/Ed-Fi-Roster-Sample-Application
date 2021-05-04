using System.Threading.Tasks;
using EdFi.Roster.Data;
using EdFi.Roster.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdFi.Roster.Explorer.Controllers
{
    public class SectionsController : Controller
    {
        private readonly SectionService sectionsService;

        public SectionsController()
        {
            sectionsService = new SectionService(new JsonFileDataService());
        }
        public async Task<IActionResult> Index()
        {
            return View(await sectionsService.ReadAll());
        }
        public async Task<IActionResult> LoadSections()
        {
            var response = await sectionsService.GetAllSectionsWithExtendedInfoAsync();
            sectionsService.Save(response.FullDataSet);
            ViewData["sectionExtendedResponseInfo"] = response;
            return View("Index", response.FullDataSet);
        }
    }
}
