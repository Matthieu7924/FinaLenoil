using _01_Controllers.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _01_Controllers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config; // Injection de dépendance par constructeur d'un objet de type IConfiguration permettant d'accéder à la configuration
        }

        // 3 ACTIONS PAR DEFAUT DANS LE CONTROLLER 'HomeController' : Index, Privacy, Error

        [ResponseCache(Duration = 5)]
        public IActionResult Index()
        {
            // throw new Exception("NEW EXCEPTION TO TEST EXCAPETION HANDLER REDIRECTION");

            _logger.LogTrace("Trace Log from HomeController"); // 0
            _logger.LogDebug("Debug Log from HomeController"); // 1
            _logger.LogInformation("Information Log from HomeController"); // 2 
            _logger.LogWarning("Warning Log from HomeController"); // 3 
            _logger.LogError("Error Log from HomeController"); // 4
            _logger.LogCritical("Critical Log from HomeController"); // 5

            string fromConfig = _config.GetValue<string>("testSettings");

            // Par défaut, la vue retournée est une vue portant le même nom que l'action
            // et située dans un dossier portant le nom du Controller (sans le mot Controller...)
            // Ce dossier étant lui même placé dans le dossier 'Views'
            // return View(); 
            
            return View("Index", fromConfig); 
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}