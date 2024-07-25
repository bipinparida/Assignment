using CloudVOffice.Core.Domain.Common;
using CloudVOffice.Core.Domain.Comunication;
using CloudVOffice.Data.DTO.Comunication;
using CloudVOffice.Services.Comunication;
using CloudVOffice.Web.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudVOffice.Web.Areas.Setup.Controllers
{
    [Area(AreaNames.Setup)]
    public class EmailAccountController : Controller
    {
        private readonly IEmailAccountService _emailAccountService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IEmailDomainService _emailDomainService;
        public EmailAccountController(IEmailAccountService emailAccountService, IWebHostEnvironment hostingEnvironment, IEmailDomainService emailDomainService)
        {
            _emailAccountService = emailAccountService;
            _hostingEnvironment = hostingEnvironment;
            _emailDomainService = emailDomainService;
        }


        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult EmailAccountCreate(int? EmailAccountId)
        {
            EmailAccountDTO emailAccountDTO = new EmailAccountDTO();

            var emailDomain = _emailDomainService.GetEmailDomains();
            ViewBag.EmailDomains = emailDomain;

            if (EmailAccountId != null)
            {

                EmailAccount d = _emailAccountService.GetEmailAccountByEmailAccountId(int.Parse(EmailAccountId.ToString()));

                emailAccountDTO.EmailAddress = d.EmailAddress;
                emailAccountDTO.Domain = d.Domain;
                emailAccountDTO.EmailAccountName = d.EmailAccountName;
                emailAccountDTO.EmailPassword = d.EmailPassword;
                emailAccountDTO.AlternativeEmailAddress = d.AlternativeEmailAddress;
                emailAccountDTO.EmailSignature = d.EmailSignature;
                emailAccountDTO.EmailLogo = d.EmailLogo;
                emailAccountDTO.IsDefaultSending = d.IsDefaultSending;
            }

            return View("~/Areas/Setup/Views/EmailAccount/EmailAccountCreate.cshtml", emailAccountDTO);

        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult EmailAccountCreate(EmailAccountDTO emailAccountDTO)
        {
            emailAccountDTO.CreatedBy = (int)Int64.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value.ToString());
            if (ModelState.IsValid)
            {
                if (emailAccountDTO.EmailLogoUp != null)
                {
                    FileInfo fileInfo = new FileInfo(emailAccountDTO.EmailLogoUp.FileName);
                    string extn = fileInfo.Extension.ToLower();
                    Guid id = Guid.NewGuid();
                    string filename = id.ToString() + extn;

                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/setup");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + filename;
                    string imagePath = Path.Combine(uploadsFolder, uniqueFileName);
                    emailAccountDTO.EmailLogoUp.CopyTo(new FileStream(imagePath, FileMode.Create));
                    emailAccountDTO.EmailLogo = uniqueFileName;
                }
                if (emailAccountDTO.EmailAccountId == null)
                {
                    var a = _emailAccountService.EmailAccountCreate(emailAccountDTO);
                    if (a == MessageEnum.Success)
                    {
                        TempData["msg"] = MessageEnum.Success;
                        return Redirect("/Setup/EmailAccount/EmailAccountView");
                    }
                    else if (a == MessageEnum.Duplicate)
                    {
                        TempData["msg"] = MessageEnum.AlreadyCreate;
                        ModelState.AddModelError("", "Emaiil Account Already Exists");
                    }
                    else
                    {
                        TempData["msg"] = MessageEnum.UnExpectedError;
                        ModelState.AddModelError("", "Un-Expected Error");
                    }
                }
                else
                {
                    var a = _emailAccountService.EmailAccountUpdate(emailAccountDTO);
                    if (a == MessageEnum.Updated)
                    {
                        TempData["msg"] = MessageEnum.Updated;
                        return Redirect("/Setup/EmailAccount/EmailAccountView");
                    }
                    else if (a == MessageEnum.Duplicate)

                    {
                        TempData["msg"] = MessageEnum.Duplicate;
                        ModelState.AddModelError("", "Email Account Already Exists");
                    }
                    else
                    {
                        TempData["msg"] = MessageEnum.UnExpectedError;
                        ModelState.AddModelError("", "Un-Expected Error");
                    }
                }
            }

            var emailDomain = _emailDomainService.GetEmailDomains();
            ViewBag.EmailDomains = emailDomain;


            return View("~/Areas/Setup/Views/EmailAccount/EmailAccountCreate.cshtml", emailAccountDTO);
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult EmailAccountView()
        {
            ViewBag.emailAccounts = _emailAccountService.GetEmailAccounts();

            return View("~/Areas/Setup/Views/EmailAccount/EmailAccountView.cshtml");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteEmailAccount(int emailAccountId)
        {
            int DeletedBy = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value.ToString());

            var a = _emailAccountService.EmailAccountDelete(emailAccountId, DeletedBy);
            return Redirect("/Setup/EmailAccount/EmailAccountView");
        }
    }


}