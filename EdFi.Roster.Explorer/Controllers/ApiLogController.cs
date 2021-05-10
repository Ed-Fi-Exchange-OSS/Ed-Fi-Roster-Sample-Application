using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EdFi.Roster.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdFi.Roster.Explorer.Controllers
{
    public class ApiLogController : Controller
    {
        private readonly ApiLogService apiLogService;

        public ApiLogController()
        {
            apiLogService = new ApiLogService();
        }
        public async Task<IActionResult> Index()
        {
            var logs = (await apiLogService.ReadAllAsync()).OrderByDescending(l => l.LogDateTime);
            return View(logs);
        }

        public IActionResult ClearLogs()
        {
            apiLogService.ClearLogs();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RawLog()
        {
            var logs = (await apiLogService.ReadAllAsync()).OrderByDescending(l => l.LogDateTime);
            return View("RawLog", JsonSerializer.Serialize(logs, options: new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
