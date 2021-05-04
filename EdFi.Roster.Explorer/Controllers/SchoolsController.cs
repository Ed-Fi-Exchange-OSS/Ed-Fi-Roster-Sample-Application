using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using EdFi.Roster.Data;
using EdFi.Roster.Services;
using EdFi.Roster.Models;
using EdFi.Roster.Sdk.Models.EnrollmentComposites;

namespace EdFi.Roster.Explorer.Controllers
{
    public class SchoolsController : Controller
    {
        private readonly SchoolService schoolService;

        public SchoolsController()
        {
            this.schoolService = new SchoolService(new JsonFileDataService());
        }

        public async Task<IActionResult> Index()
        {
            //Read any saved Schools previously saved to be displayed
            return View(await schoolService.ReadAll());
        }

        public async Task<IActionResult> LoadSchools()
        {
            var response = await schoolService.GetAllSchoolsWithExtendedInfoAsync();
            schoolService.Save(response.FullDataSet);
            ViewData["schoolExtendedResponseInfo"] = response;
            return View("Index", response.FullDataSet);
        }
    }

}
