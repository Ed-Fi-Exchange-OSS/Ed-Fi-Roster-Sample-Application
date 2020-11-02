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
        public IActionResult Index()
        {
            return View(sectionsService.ReadAll());
        }
        public IActionResult LoadSections()
        {
            var response = sectionsService.GetAllSectionsWithExtendedInfoAsync().Result;
            sectionsService.Save(response.FullDataSet);
            ViewData["sectionExtendedResponseInfo"] = response;
            return View("Index", response.FullDataSet);
        }
    }
}
