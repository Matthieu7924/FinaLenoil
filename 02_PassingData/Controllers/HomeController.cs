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
            if (TempData.ContainsKey("tmpDataFromAction"))
            {
                ViewBag.tmpDataFromAction = TempData["tmpDataFromAction"]; // La clé 'tmpDataFromAction' est détruite après avoir été lue (mais pas avant...)
            }

            if (TempData.ContainsKey("tmpDataFromView"))
            {
                ViewBag.tmpDataFromView = TempData["tmpDataFromView"]; // La clé 'tmpDataFromView' est détruite après avoir été lue (mais pas avant...)
            }

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

            // ATTENTION ViewBag.Message <=> ViewData["Message"]
            // Une propriété du ViewBag est reconnue en tant que clé du ViewData et inversement

            ViewData["key"] = "Your application privacy page from ViewData";

            IList<string> list = new List<string>
             {
                 "riri",
                 "fifi",
                 "loulou"
             };
            // IList<string> list = null;
            ViewData["stringlist"] = list;

            /*
             * TempData permet de trasnférer des données :
             * - d'une vue à un controlleur 
             * - d'un controlleur à une vue
             * - d'une méthode d'action à une autre méthode d'ction du même controlleur ou d'un controlleur différent.
             * 
             * TempData stocke temporairement les données et les supprime automatiquement après qu'elles aient été lues.
             */
            TempData["tmpDataFromAction"] = "Data from Action 'Privacy' in 'HomeController' passed with TempData";


            // Session : 
            // - objet de type dictionnaire créé côté serveur
            // - accessible par l'ensemble des controlleurs et des vues
            // - expire par défaut au bout de 20min...

            // ATTENTION : il faut rajouter 
            // - builder.Services.AddSession();
            // - app.UseSession(); // dans le pipeline
            // Sinon : System.InvalidOperationException : 'Session has not been configured for this application or request.'
            // 

            HttpContext.Session.SetString("user_name", "riri");
            HttpContext.Session.SetInt32("user_id", 12);

            // Peut être prématurément vidé de tout ou partie de son contenu
            HttpContext.Session.Remove("user_id"); // supprime juste la clé 'user_id'
            HttpContext.Session.Clear(); // Supprime (toutes les clés) de la Session

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}