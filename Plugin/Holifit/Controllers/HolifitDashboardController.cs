using CloudVOffice.Web.Framework;
using CloudVOffice.Web.Framework.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holifit.Controllers
{
    [Area(AreaNames.Holifit)]
    public class HolifitDashboardController : BasePluginController
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public HolifitDashboardController(IWebHostEnvironment hostEnvironment)

        {
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {

            return View("~/Plugins/Holifit/Views/HolifitDashboard/Dashboard.cshtml");
        }
    }
}
