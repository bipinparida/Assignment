using CloudVOffice.Core.Domain.Common;
using CloudVOffice.Core.Domain.Company;
using CloudVOffice.Data.DTO.Company;
using CloudVOffice.Services.Company;
using CloudVOffice.Web.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudVOffice.Web.Areas.Setup.Controllers
{
    [Area(AreaNames.Setup)]
    public class LetterHeadController : Controller
    {
        private readonly ILetterHeadService _letterHeadService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public LetterHeadController(ILetterHeadService letterHeadService, IWebHostEnvironment hostEnvironment)
        {

            _letterHeadService = letterHeadService;
            _hostingEnvironment = hostEnvironment;
        }
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult LetterHeadCreate(int? letterHeadId)
        {
            LetterHeadDTO letterHeadDTO = new LetterHeadDTO();

            if (letterHeadId != null)
            {

                LetterHead d = _letterHeadService.GetLetterHeadByLetterHeadId(int.Parse(letterHeadId.ToString()));

                letterHeadDTO.LetterHeadName = d.LetterHeadName;
                letterHeadDTO.LetterHeadImage = d.LetterHeadImage;
                letterHeadDTO.LetterHeadImageHeight = d.LetterHeadImageHeight;
                letterHeadDTO.LetterHeadImageWidth = d.LetterHeadImageWidth;
                letterHeadDTO.LetterHeadAlign = d.LetterHeadAlign;
                letterHeadDTO.LetterHeadFooterImage = d.LetterHeadFooterImage;
                letterHeadDTO.LetterHeadImageFooterHeight = d.LetterHeadImageFooterHeight;
                letterHeadDTO.LetterHeadImageFooterWidth = d.LetterHeadImageFooterWidth;
                letterHeadDTO.LetterHeadFooterAlign = d.LetterHeadFooterAlign;


            }
            else
            {
                if (_letterHeadService.GetLetter() != null)
                {
                    TempData["msg"] = MessageEnum.AlreadyCreate;
                    return Redirect("/Setup/LetterHead/LetterHeadView");
                }


            }

            return View("~/Areas/Setup/Views/LetterHead/LetterHeadCreate.cshtml", letterHeadDTO);

        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult LetterHeadCreate(LetterHeadDTO letterHeadDTO)
        {
            letterHeadDTO.CreatedBy = int.Parse(User.Claims.SingleOrDefault(x => x.Type == "UserId").Value.ToString());

            if (ModelState.IsValid)
            {

                if (letterHeadDTO.LetterHeadImageUp != null)
                {
                    FileInfo fileInfo = new FileInfo(letterHeadDTO.LetterHeadImageUp.FileName);
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
                    letterHeadDTO.LetterHeadImageUp.CopyTo(new FileStream(imagePath, FileMode.Create));
                    letterHeadDTO.LetterHeadImage = uniqueFileName;
                }

                if (letterHeadDTO.LetterHeadFooterImageUP != null)
                {
                    FileInfo fileInfo = new FileInfo(letterHeadDTO.LetterHeadFooterImageUP.FileName);
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
                    letterHeadDTO.LetterHeadFooterImageUP.CopyTo(new FileStream(imagePath, FileMode.Create));
                    letterHeadDTO.LetterHeadFooterImage = uniqueFileName;
                }

                if (letterHeadDTO.LetterHeadId == null)
                {


                    var a = _letterHeadService.LetterHeadCreate(letterHeadDTO);
                    if (a == MessageEnum.Success)
                    {
                        TempData["msg"] = MessageEnum.Success;
                        return Redirect("/Setup/LetterHead/LetterHeadView");
                    }
                    else if (a == MessageEnum.AlreadyCreate)
                    {
                        TempData["msg"] = MessageEnum.AlreadyCreate;
                        ModelState.AddModelError("", "LetterHead Already Exists");
                    }
                    else
                    {
                        TempData["msg"] = MessageEnum.UnExpectedError;
                        ModelState.AddModelError("", "Un-Expected Error");
                    }
                }
                else
                {

                    var a = _letterHeadService.LetterHeadUpdate(letterHeadDTO);
                    if (a == MessageEnum.Updated)
                    {
                        TempData["msg"] = MessageEnum.Updated;
                        return Redirect("/Setup/LetterHead/LetterHeadView");
                    }

                    else
                    {
                        TempData["msg"] = MessageEnum.UnExpectedError;
                        ModelState.AddModelError("", "Un-Expected Error");
                    }
                }
            }


            return View("~/Areas/Setup/Views/LetterHead/LetterHeadCreate.cshtml", letterHeadDTO);
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult LetterHeadView()
        {
            ViewBag.letterHeads = _letterHeadService.GetLetterHeads();

            return View("~/Areas/Setup/Views/LetterHead/LetterHeadView.cshtml");
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult LetterHeadDelete(int letterHeadId)
        {
            int DeletedBy = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value.ToString());

            var a = _letterHeadService.LetterHeadDelete(letterHeadId, DeletedBy);
            TempData["msg"] = a;
            return Redirect("/Setup/LetterHead/LetterHeadView");
        }
    }
}
