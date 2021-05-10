using System.Threading.Tasks;
using EdFi.Roster.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdFi.Roster.Explorer.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentService _studentService;
        public StudentsController(StudentService studentService)
        {
            _studentService = studentService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _studentService.ReadAllAsync());
        }
        public async Task<IActionResult> LoadStudents()
        {
            var response = await _studentService.GetAllStudentsWithExtendedInfoAsync();
            await _studentService.Save(response.FullDataSet);
            ViewData["studentExtendedResponseInfo"] = response;
            return View("Index", response.FullDataSet);
        }
    }
}
