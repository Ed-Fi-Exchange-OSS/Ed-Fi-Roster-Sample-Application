using System;
using System.Collections.Generic;
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
        public IActionResult Index()
        {
            var logs = apiLogService.ReadAll().OrderByDescending(l => l.LogDateTime);
            return View(logs);
        }

        public IActionResult ClearLogs()
        {
            apiLogService.ClearLogs();
            return RedirectToAction("Index");
        }

        public IActionResult RawLog()
        {
            var logs = apiLogService.ReadAll().OrderByDescending(l => l.LogDateTime);
            return View("RawLog", JsonSerializer.Serialize(logs, options: new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
