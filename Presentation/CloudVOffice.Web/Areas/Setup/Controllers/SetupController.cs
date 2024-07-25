using CloudVOffice.Core.Domain.Users;
using CloudVOffice.Services.Company;
using CloudVOffice.Services.Comunication;
using CloudVOffice.Services.Users;
using CloudVOffice.Web.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudVOffice.Web.Areas.Setup.Controllers
{
    [Area(AreaNames.Setup)]
    public class SetupController : Controller
    {
        private readonly ICompanyDetailsService _companyDetailsService;
        private readonly IUserService _userService;
        private readonly ILetterHeadService _letterHeadService;
        private readonly IEmailDomainService _emailDomainService;
        private readonly IEmailAccountService _emailAccountService;
        //   private required 
        public SetupController(ICompanyDetailsService companyDetailsService,

             IUserService userService,
              ILetterHeadService letterHeadService,
              IEmailDomainService emailDomainService,
              IEmailAccountService emailAccountService
            )
        {
            _companyDetailsService = companyDetailsService;
            _userService = userService;
            _letterHeadService = letterHeadService;
            _emailDomainService = emailDomainService;
            _emailAccountService = emailAccountService;

        }
        [Authorize(Roles = "Administrator")]
        public IActionResult DashBoard()
        {
            ViewBag.company = _companyDetailsService.GetCompanyDetails();
            ViewBag.LetterHead = _letterHeadService.GetLetter();
            ViewBag.EmailDomain = _emailDomainService.GetEmailDomains();
            ViewBag.EmailAccounts = _emailAccountService.GetEmailAccounts();
            ViewBag.AllUsers = _userService.GetAllUsers();
            ViewBag.ActiveUsers = _userService.GetAllUsers().Where(x => x.IsActive == true).ToList();
            ViewBag.InActiveUsers = _userService.GetAllUsers().Where(x => x.IsActive == false).ToList();
            ViewBag.SystemUsers = _userService.GetAllUsers().Where(x => x.UserType == UserType.SystemUser).ToList();
            return View();
        }
    }
}
