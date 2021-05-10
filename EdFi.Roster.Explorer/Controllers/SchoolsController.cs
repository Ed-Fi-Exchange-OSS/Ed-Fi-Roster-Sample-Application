using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EdFi.Roster.Services;

namespace EdFi.Roster.Explorer.Controllers
{
    public class SchoolsController : Controller
    {
        private readonly SchoolService _schoolService;

        public SchoolsController(SchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        public async Task<IActionResult> Index()
        {
            //Read any saved Schools previously saved to be displayed
            return View(await _schoolService.ReadAllAsync());
        }

        public async Task<IActionResult> LoadSchools()
        {
            var response = await _schoolService.GetAllSchoolsWithExtendedInfoAsync();
            await _schoolService.Save(response.FullDataSet);
            ViewData["schoolExtendedResponseInfo"] = response;
            return View("Index", response.FullDataSet);
        }
    }

}
