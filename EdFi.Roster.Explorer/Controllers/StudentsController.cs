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
        public IActionResult Index()
        {
            return View(studentService.ReadAll());
        }
        public IActionResult LoadStudents()
        {
            var response = studentService.GetAllStudentsWithExtendedInfoAsync().Result;
            studentService.Save(response.FullDataSet);
            ViewData["studentExtendedResponseInfo"] = response;
            return View("Index", response.FullDataSet);
        }
    }
}
