using System.Threading.Tasks;
using EdFi.Roster.Data;
using EdFi.Roster.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdFi.Roster.Explorer.Controllers
{
    public class StaffController : Controller
    {
        private readonly StaffService staffService;
        public StaffController()
        {
            staffService = new StaffService(new JsonFileDataService());
        }

        public async Task<IActionResult> Index()
        {
            return View(await staffService.ReadAll());
        }
        public async Task<IActionResult> LoadStaff()
        {
            var response = await staffService.GetAllStaffWithExtendedInfoAsync();
            staffService.Save(response.FullDataSet);
            ViewData["staffExtendedResponseInfo"] = response;
            return View("Index", response.FullDataSet);
        }
    }
}
