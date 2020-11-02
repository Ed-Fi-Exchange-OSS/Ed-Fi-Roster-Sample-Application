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

        public IActionResult Index()
        {
            return View(staffService.ReadAll());
        }
        public IActionResult LoadStaff()
        {
            var response = staffService.GetAllStaffWithExtendedInfoAsync().Result;
            staffService.Save(response.FullDataSet);
            ViewData["staffExtendedResponseInfo"] = response;
            return View("Index", response.FullDataSet);
        }
    }
}
