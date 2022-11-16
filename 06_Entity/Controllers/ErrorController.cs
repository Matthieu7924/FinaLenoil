using _06_Entity.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _06_Entity.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var StatusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource could not be found";
                    ViewBag.Path = StatusCodeResult?.OriginalPath;
                    ViewBag.QS = StatusCodeResult?.OriginalQueryString;

                    _logger.LogWarning($"404 Error Ocurred : Path = {StatusCodeResult?.OriginalPath}" +
                        $" and Query String = {StatusCodeResult?.OriginalQueryString}");

                    return View("NotFound");

                case 400:
                    ViewBag.ErrorMessage = "BAD REQUEST";
                    return View("NotFound");

                default:
                    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

        }
    }
}
