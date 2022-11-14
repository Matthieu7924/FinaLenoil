using _04_TagHelpers.Models;
using Microsoft.AspNetCore.Mvc;

namespace _04_TagHelpers.Controllers
{
    public class EmployeController : Controller
    {
        public IActionResult Create(Employe emp)
        {
            return View(emp);
        }

        public IActionResult Bonjour(Employe emp)
        {
            return View(emp);
        }
    }
}
