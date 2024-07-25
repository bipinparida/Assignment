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
    public class EmailDomainController : Controller
    {

        private readonly IEmailDomainService _emailDomainService;
        public EmailDomainController(IEmailDomainService emailDomainService)
        {

            _emailDomainService = emailDomainService;
        }
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult EmailDomainCreate(int? emailDomainId)
        {
            EmailDomainDTO emailDomainDTO = new EmailDomainDTO();

            if (emailDomainId != null)
            {

                EmailDomain d = _emailDomainService.GetEmailDomainByEmailDomainId(int.Parse(emailDomainId.ToString()));

                emailDomainDTO.DomainName = d.DomainName;
                emailDomainDTO.IncomingServer = d.IncomingServer;
                emailDomainDTO.IncomingPort = d.IncomingPort;
                emailDomainDTO.IncomingIsIMAP = d.IncomingIsIMAP;
                emailDomainDTO.IncomingIsSsl = d.IncomingIsSsl;
                emailDomainDTO.IncomingIsStartTLs = d.IncomingIsStartTLs;
                emailDomainDTO.OutingServer = d.OutingServer;
                emailDomainDTO.OutgoingPort = d.OutgoingPort;
                emailDomainDTO.OutgoingIsTLs = d.OutgoingIsTLs;
                emailDomainDTO.OutgoingIsSsl = d.OutgoingIsSsl;

            }
            else
            {
                if (_emailDomainService.GetEmailDomains() == null)
                {
                    TempData["msg"] = MessageEnum.AlreadyCreate;
                    return Redirect("/Setup/EmailDomain/EmailDomainView");
                }


            }

            return View("~/Areas/Setup/Views/EmailDomain/EmailDomainCreate.cshtml", emailDomainDTO);

        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult EmailDomainCreate(EmailDomainDTO emailDomainDTO)
        {
            emailDomainDTO.CreatedBy = (int)Int64.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value.ToString());


            if (ModelState.IsValid)
            {
                if (emailDomainDTO.EmailDomainId == null)
                {
                    var a = _emailDomainService.EmailDomainCreate(emailDomainDTO);
                    if (a != null)
                    {
                        if (a == MessageEnum.Success)
                        {
                            TempData["msg"] = MessageEnum.Success;
                            return Redirect("/Setup/EmailDomain/EmailDomainView");
                        }
                        else if (a == MessageEnum.AlreadyCreate)
                        {
                            TempData["msg"] = MessageEnum.AlreadyCreate;

                            ModelState.AddModelError("", "EmailDomain Already Exists");
                        }
                        else
                        {
                            TempData["msg"] = MessageEnum.UnExpectedError;
                            ModelState.AddModelError("", "Un-Expected Error");
                        }
                    }
                }
                else
                {
                    var a = _emailDomainService.EmailDomainUpdate(emailDomainDTO);
                    if (a == MessageEnum.Updated)
                    {
                        TempData["msg"] = MessageEnum.Updated;
                        return Redirect("/Setup/EmailDomain/EmailDomainView");
                    }
                    else if (a == MessageEnum.Duplicate)
                    {
                        TempData["msg"] = MessageEnum.Duplicate;
                        ModelState.AddModelError("", "EmailDomain Already Exists");
                    }
                    else
                    {
                        TempData["msg"] = MessageEnum.UnExpectedError;
                        ModelState.AddModelError("", "Un-Expected Error");
                    }
                }
            }


            return View("~/Areas/Setup/Views/EmailDomain/EmailDomainCreate.cshtml", emailDomainDTO);
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult EmailDomainView()
        {
            ViewBag.emailDomains = _emailDomainService.GetEmailDomains();

            return View("~/Areas/Setup/Views/EmailDomain/EmailDomainView.cshtml");
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult EmailDomainDelete(int emailDomainId)
        {
            int DeletedBy = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value.ToString());

            var a = _emailDomainService.EmailDomainDelete(emailDomainId, DeletedBy);

            return Redirect("/Setup/EmailDomain/EmailDomainView");
        }
    }
}
