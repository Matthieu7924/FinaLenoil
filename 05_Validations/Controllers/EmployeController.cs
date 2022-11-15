using Microsoft.AspNetCore.Mvc;
using Validations.Models;

namespace Validations.Controllers
{
    public class EmployeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironement;

        public EmployeController(IWebHostEnvironment webHostEnvironement)
        {
            _webHostEnvironement = webHostEnvironement;
        }

        [HttpGet]
        public IActionResult FormValidation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Permet de nous protéger des attaques de type cross-site request forgery
        public async Task<IActionResult> FormValidation(Employe emp)
        {
            if (ModelState.IsValid)
            {
                if (emp.Avatar is not null && emp.Avatar.Length > 0)
                {
                    string upload = Path.Combine(_webHostEnvironement.WebRootPath, "uploads");
                    string filePath = Path.Combine(upload, emp.Avatar.FileName);

                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await emp.Avatar.CopyToAsync(fileStream);

                        // fileStream.Close(); // Fait automatiquement car 'fileStream' est créé dans clause 'using'
                    }
                }
                return View("Details", emp);
            }

            return View(emp);
        }
    }
}
