using Microsoft.AspNetCore.Mvc;

namespace CloudVOffice.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult NotFound()
        {
            return View();
        }

        public IActionResult UnAuthorized()
        {
            return View();
        }
        public IActionResult Forbidden()
        {
            return View();
        }
        public IActionResult InternalServerError()
        {
            return View();
        }
        public IActionResult ServiceUnavailable()
        {
            return View();
        }
        public IActionResult BadGateway()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }

}
