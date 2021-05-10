using System.Threading.Tasks;
using EdFi.Roster.Data;
using EdFi.Roster.Explorer.Models;
using EdFi.Roster.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdFi.Roster.Explorer.Controllers
{
    public class RosterController : Controller
    {
        // GET: RosterController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RosterController/Details/5
        public async Task<ActionResult> HierarchyWithTerms()
        {
            var rosterService = new RosterService(new JsonFileDataService());
            var leaRoster = await rosterService.GetRosterAsync();
            var returnRoster = new RosterViewModel(leaRoster);
            return View(returnRoster);
        }
        public async Task<ActionResult> HierarchyByStaff()
        {
            var rosterService = new RosterService(new JsonFileDataService());
            var leaRoster = await rosterService.GetRosterAsync();
            var returnRoster = new RosterViewModel(leaRoster);
            return View(returnRoster);
        }

        public async Task<ActionResult> HierarchyBySchoolSection()
        {
            var rosterService = new RosterService(new JsonFileDataService());
            var leaRoster = await rosterService.GetRosterAsync();
            var returnRoster = new RosterViewModel(leaRoster);
            return View(returnRoster);
        }
    }
}
