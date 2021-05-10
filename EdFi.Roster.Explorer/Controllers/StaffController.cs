using System.Threading.Tasks;
using EdFi.Roster.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdFi.Roster.Explorer.Controllers
{
    public class StaffController : Controller
    {
        private readonly StaffService _staffService;
        public StaffController(StaffService staffService)
        {
            _staffService = staffService; //new StaffService(new JsonFileDataService());
        }

        public async Task<IActionResult> Index()
        {
            return View(await _staffService.ReadAllAsync());
        }
        public async Task<IActionResult> LoadStaff()
        {
            var response = await _staffService.GetAllStaffWithExtendedInfoAsync();
            await _staffService.Save(response.FullDataSet);
            ViewData["staffExtendedResponseInfo"] = response;
            return View("Index", response.FullDataSet);
        }
    }
}
