using System.Threading.Tasks;
using EdFi.Roster.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdFi.Roster.Explorer.Controllers
{
    public class SectionsController : Controller
    {
        private readonly SectionService _sectionsService;

        public SectionsController(SectionService sectionService)
        {
            _sectionsService = sectionService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _sectionsService.ReadAllAsync());
        }
        public async Task<IActionResult> LoadSections()
        {
            var response = await _sectionsService.GetAllSectionsWithExtendedInfoAsync();
            await _sectionsService.Save(response.FullDataSet);
            ViewData["sectionExtendedResponseInfo"] = response;
            return View("Index", response.FullDataSet);
        }
    }
}
