using TagHelpers.Models;
using Microsoft.AspNetCore.Mvc;

namespace TagHelpers.Controllers
{
    public class EmployeController : Controller
    {
        private readonly ILogger<EmployeController> _logger;

        public EmployeController(ILogger<EmployeController> logger)
        {
            _logger = logger;
        }

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
