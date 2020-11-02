using System.Threading.Tasks;
using EdFi.Roster.Data;
using EdFi.Roster.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdFi.Roster.Explorer.Controllers
{
    public class GetFullRosterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetLeasAsync")]
        public async Task<IActionResult> GetLeasAsync()
        {
            var service = new LocalEducationAgencyService(new JsonFileDataService());
            var response = await service.GetAllLocalEducationAgenciesWithExtendedInfoAsync();
            service.Save(response.FullDataSet);
            return Ok(response);
        }

        [HttpGet("GetSchoolsAsync")]
        public async Task<IActionResult> GetSchoolsAsync()
        {
            var service = new SchoolService(new JsonFileDataService());
            var response = await service.GetAllSchoolsWithExtendedInfoAsync();
            service.Save(response.FullDataSet);
            return Ok(response);
        }

        [HttpGet("GetSectionsAsync")]
        public async Task<IActionResult> GetSectionsAsync()
        {
            var service = new SectionService(new JsonFileDataService());
            var response = await service.GetAllSectionsWithExtendedInfoAsync();
            service.Save(response.FullDataSet);
            return Ok(response);
        }

        [HttpGet("GetStaffAsync")]
        public async Task<IActionResult> GetStaffAsync()
        {
            var service = new StaffService(new JsonFileDataService());
            var response = await service.GetAllStaffWithExtendedInfoAsync();
            service.Save(response.FullDataSet);
            return Ok(response);
        }

        [HttpGet("GetStudentsAsync")]
        public async Task<IActionResult> GetStudentsAsync()
        {
            var service = new StudentService(new JsonFileDataService());
            var response = await service.GetAllStudentsWithExtendedInfoAsync();
            service.Save(response.FullDataSet);
            return Ok(response);
        }

    }
}
