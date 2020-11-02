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
        public ActionResult HierarchyWithTerms()
        {
            var rosterService = new RosterService(new JsonFileDataService());
            var leaRoster = rosterService.GetRoster();
            var returnRoster = new RosterViewModel(leaRoster);
            return View(returnRoster);
        }
        public ActionResult HierarchyByStaff()
        {
            var rosterService = new RosterService(new JsonFileDataService());
            var leaRoster = rosterService.GetRoster();
            var returnRoster = new RosterViewModel(leaRoster);
            return View(returnRoster);
        }

        public ActionResult HierarchyBySchoolSection()
        {
            var rosterService = new RosterService(new JsonFileDataService());
            var leaRoster = rosterService.GetRoster();
            var returnRoster = new RosterViewModel(leaRoster);
            return View(returnRoster);
        }
    }
}
