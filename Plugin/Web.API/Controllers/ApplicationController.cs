using CloudVOffice.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    //
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApplicationController : Controller
    {
        private readonly IUserService _userService;
        public ApplicationController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetUserMenu()
        {
            var a = _userService.GetUserMenu(Int64.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value.ToString()));

            return View(a);
        }


    }
}
