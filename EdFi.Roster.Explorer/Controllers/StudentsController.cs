using System.Threading.Tasks;
using EdFi.Roster.Data;
using EdFi.Roster.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdFi.Roster.Explorer.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentService studentService;
        public StudentsController()
        {
            studentService = new StudentService(new JsonFileDataService());
        }
        public async Task<IActionResult> Index()
        {
            return View(await studentService.ReadAll());
        }
        public async Task<IActionResult> LoadStudents()
        {
            var response = await studentService.GetAllStudentsWithExtendedInfoAsync();
            studentService.Save(response.FullDataSet);
            ViewData["studentExtendedResponseInfo"] = response;
            return View("Index", response.FullDataSet);
        }
    }
}
