using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace _01_Controllers.Controllers
{
    // [Route("new_route_controller")]
    public class NewController : Controller // https://localhost:7279/new_route/redirected
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NewController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        // [Route("new_route_controller/new_route_action")] // routage par attributs
        // [Route("new_route_action")] // routage par attributs
        public string ActionReturnString(int? id)
        {
            string first = HttpContext.Request.Query["firstname"];
            string last = HttpContext.Request.Query["lastname"];

            return $"Hello from action 'ActionReturnString' of Controller 'new' with id = {id} and " +
                $"query string 'firstname' = {first} and query string 'lastname' = {last}";
        }

        public ViewResult ActionReturnView()
        {
            return View(); // Retourne une vue qui porte le même nom que l'action courante dans le controlleur courant
        }

        public ViewResult ActionReturnSpecifiView()
        {
            return View("SpecificView");
        }

        public ActionResult ActionReturnRedirectToAction()
        {
            return RedirectToAction("ActionReturnView");
        }

        public ActionResult ActionReturnRedirectToActionWithParameters()
        {
            return RedirectToAction("ActionReturnString", new { @id = 99, @firstname ="toto" });
        }

        public ActionResult ActionReturnContent()
        {
            return Content("<div>Contenu de Action Return Content</div>", "text/html");
        }

        public ActionResult ActionReturnJavaScript()
        {
            return Content("<script>alert(\"Action Return Javascript\")</script>", "text/html");
        }

        public ActionResult ActionReturnNotFound()
        {
            return NotFound();
        }

        public ActionResult ActionReturnHttpStatusCodeResult()
        {
            return StatusCode(StatusCodes.Status400BadRequest, "Mauvaise requête");
        }

        public FileResult ActionReturnFileResult()
        {
            string fileName = "site.css";

            // Construit le nom complet du fichier à retourner
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "css/") + fileName;

            // Lit le contenu du fichier dans un tableau de bytes
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Envoie le fichier à télécharger
            return File(bytes, "application/octet-stream", fileName);
        }
    }
}
