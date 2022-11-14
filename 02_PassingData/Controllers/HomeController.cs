using _02_PassingData.Models;
using _02_PassingData.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _02_PassingData.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Personnage p = new() { Prenom = "riri", Nom = "Duck" };

            // La vue "Index" prend en paramètre un objet fortement typé de type "Personnage"
            return View(p);
        }

        public IActionResult Privacy()
        {

            /*
             * Le ViewBag permet de transférer des données d'un Controlleur à une Vue
             * Ces données sont transférées en tant que propriétés de l'objet ViewBag
             * La portée est limitée à la requête actuelle : sa valeur est réinitialisée après avoir été transférée à la Vue
             */
            ViewBag.Message = "Your application privacy page from ViewBag";


            /*
             * ViewData est un objet dictionnaire permettant de transférer des données d'un Controlleur à une Vue.
             * Ces données sont transférées sous form d'une paire clé-valeur.
             * La portée est limitée à la requête actuelle : sa valeur est réinitialisée après avoir été transférée à la Vue
             * 
             * Si on passe un objet de type référence dans le ViewDate, il faut vérifier dans la vue que l'objet passé n'est pas null
             * Sinon risque de NullReferencePointerException
             */
            ViewData["key"] = "Your application privacy page from ViewData";

            IList<string> list = new List<string>
             {
                 "riri",
                 "fifi",
                 "loulou"
             };
            // IList<string> list = null;
            ViewData["stringlist"] = list;


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}