using Microsoft.AspNetCore.Mvc;

namespace StudentGrades.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Redirect to Students index by default
            return RedirectToAction("Index", "Students");
        }
    }
}
