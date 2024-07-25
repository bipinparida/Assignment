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
    public class CompanyDetailsController : Controller
    {
        private readonly ICompanyDetailsService _companyDetailsService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public CompanyDetailsController(ICompanyDetailsService companyDetailsService, IWebHostEnvironment hostingEnvironment)
        {

            _companyDetailsService = companyDetailsService;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult CompanyDetailsCreate(int? companyDetailsId)
        {
            CompanyDetailsDTO companyDetailsDTO = new CompanyDetailsDTO();


            if (companyDetailsId != null)
            {

                CompanyDetails d = _companyDetailsService.GetCompanyDetailsByCompanyDetailsId(int.Parse(companyDetailsId.ToString()));

                companyDetailsDTO.CompanyName = d.CompanyName;
                companyDetailsDTO.ABBR = d.ABBR;
                companyDetailsDTO.CompanyLogo = d.CompanyLogo;
                companyDetailsDTO.TaxId = d.TaxId;
                companyDetailsDTO.Domain = d.Domain;
                companyDetailsDTO.DateOfEstablishment = d.DateOfEstablishment;
                companyDetailsDTO.DateOfIncorporation = d.DateOfIncorporation;
                companyDetailsDTO.AddressLine1 = d.AddressLine1;
                companyDetailsDTO.AddressLine2 = d.AddressLine2;
                companyDetailsDTO.City = d.City;
                companyDetailsDTO.State = d.State;
                companyDetailsDTO.Country = d.Country;
                companyDetailsDTO.PostalCode = d.PostalCode;
                companyDetailsDTO.EmailAddress = d.EmailAddress;
                companyDetailsDTO.PhoneNumber = d.PhoneNumber;
                companyDetailsDTO.Fax = d.Fax;
                companyDetailsDTO.Website = d.Website;
            }
            else
            {

                if (_companyDetailsService.GetCompanyDetails() != null)
                {
                    TempData["msg"] = MessageEnum.AlreadyCreate;
                    return Redirect("/Setup/CompanyDetails/CompanyDetailsView");
                }

            }
            return View("~/Areas/Setup/Views/CompanyDetails/CompanyDetailsCreate.cshtml", companyDetailsDTO);

        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult CompanyDetailsCreate(CompanyDetailsDTO companyDetailsDTO)
        {
            companyDetailsDTO.CreatedBy = int.Parse(User.Claims.SingleOrDefault(x => x.Type == "UserId").Value.ToString());

            if (ModelState.IsValid)
            {
                if (companyDetailsDTO.CompanyLogoUp != null)
                {
                    FileInfo fileInfo = new FileInfo(companyDetailsDTO.CompanyLogoUp.FileName);
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
                    companyDetailsDTO.CompanyLogoUp.CopyTo(new FileStream(imagePath, FileMode.Create));
                    companyDetailsDTO.CompanyLogo = uniqueFileName;
                }
                if (companyDetailsDTO.CompanyDetailsId == null)
                {
                    var a = _companyDetailsService.CompanyDetailsCreate(companyDetailsDTO);
                    if (a == MessageEnum.Success)
                    {
                        TempData["msg"] = MessageEnum.Success;
                        return Redirect("/Setup/CompanyDetails/CompanyDetailsView");
                    }
                    else if (a == MessageEnum.AlreadyCreate)
                    {
                        TempData["msg"] = MessageEnum.AlreadyCreate;

                        return Redirect("/Setup/CompanyDetails/CompanyDetailsView");
                    }
                    else
                    {
                        TempData["msg"] = MessageEnum.Error;
                        ModelState.AddModelError("", "Un-Expected Error");
                    }
                }
                else
                {
                    var a = _companyDetailsService.CompanyDetailsUpdate(companyDetailsDTO);
                    if (a == MessageEnum.Updated)
                    {
                        TempData["msg"] = MessageEnum.Updated;
                        return Redirect("/Setup/CompanyDetails/CompanyDetailsView");
                    }

                    else
                    {
                        TempData["msg"] = MessageEnum.UnExpectedError;
                        ModelState.AddModelError("", "Un-Expected Error");
                    }
                }
            }
            return View("~/Areas/Setup/Views/CompanyDetails/CompanyDetailsCreate.cshtml", companyDetailsDTO);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult CompanyDetailsView()
        {
            ViewBag.CompanyDetails = _companyDetailsService.GetCompanyDetailsList();

            return View("~/Areas/Setup/Views/CompanyDetails/CompanyDetailsView.cshtml");
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult CompanyDetailsDelete(int companyDetailsId)
        {
            int DeletedBy = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value.ToString());

            var a = _companyDetailsService.CompanyDetailsDelete(companyDetailsId, DeletedBy);
            TempData["msg"] = a;
            return Redirect("/Setup/CompanyDetails/CompanyDetailsView");
        }
    }
}


