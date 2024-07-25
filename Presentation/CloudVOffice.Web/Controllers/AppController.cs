using CloudVOffice.Core.Domain.Common;
using CloudVOffice.Core.Domain.Logging;
using CloudVOffice.Core.Domain.Users;
using CloudVOffice.Data.DTO.Users;
using CloudVOffice.Data.Persistence;
using CloudVOffice.Services.Authentication;
using CloudVOffice.Services.Company;
using CloudVOffice.Services.Roles;
using CloudVOffice.Services.Users;
using CloudVOffice.Web.Model.User;
using LinqToDB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace CloudVOffice.Web.Controllers
{
    public class AppController : Controller
    {
        private readonly IUserAuthenticationService _userauthenticationService;
        private readonly IUserService _userService;
        private readonly ICompanyDetailsService _companyDetailsService;
        private readonly IRoleService _roleService;
        private readonly ApplicationDBContext _dbContext;
		public AppController(IUserAuthenticationService userauthenticationService,
            IUserService userService,
            IRoleService roleService,
            ApplicationDBContext dbContext,
			ICompanyDetailsService companyDetailsService
            )
        {
            _userauthenticationService = userauthenticationService;
            _userService = userService;
            _roleService = roleService;
           _companyDetailsService = companyDetailsService;
            _dbContext = dbContext;

        }
        public IActionResult Login()
        {


            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
        public IActionResult PasswordResetLinkSent()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var a = _userService.SendResetPasswordEmail(model.Email);
                if (a == MessageEnum.Success)
                {
                    return LocalRedirect("/App/PasswordResetLinkSent");
                }
                else if (a == MessageEnum.Invalid)
                {
                    ModelState.AddModelError("Email", "User Not Exists.");

                }
                else if (a == MessageEnum.Invalid)
                {
                    ModelState.AddModelError("Email", "In-Active User.");

                }
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string? ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var Email = model.Email?.Trim();
                var loginResult = await _userauthenticationService.ValidateUserAsync(Email, model.Password);
                switch (loginResult)
                {

                    case UserLoginResults.Successful:
                        {
                            var userDetails = await _userService.GetUserByEmailAsync(Email);
                            var companyDetails = _companyDetailsService.GetCompanyDetails();

                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Email, userDetails.Email),
                                new Claim("FirstName",userDetails.FirstName),
                                new Claim("MiddleName",userDetails.MiddleName!=null?userDetails.MiddleName.ToString():""),
                                new Claim("LastName",userDetails.LastName!=null?userDetails.LastName.ToString():""),
                                new Claim("UserId",userDetails.UserId.ToString()),
								//  new Claim("Menu",menujson),
							};

							var activityLogs = new ActivityLog
							{

								UserId = userDetails.UserId,
								CreatedOn = DateTime.Now,
								LogInTime = DateTime.Now,
								EntityName = "Login"
							};
							_dbContext.ActivityLogs.Add(activityLogs);
							await _dbContext.SaveChangesAsync();

							var a = userDetails.UserRoleMappings;

                            if (companyDetails != null)
                            {
                                claims.Add(new Claim("CompanyImage", companyDetails.CompanyLogo));
                                claims.Add(new Claim("CompanyName", companyDetails.CompanyName));
                            }
                            claims.AddRange(userDetails.UserRoleMappings.Select(role => new Claim(ClaimTypes.Role, role.Role.RoleName)));

                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var authProperties = new AuthenticationProperties() { IsPersistent = true };
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);


                            return Redirect(ReturnUrl == null ? "/Applications" : ReturnUrl);
                        }
                    case UserLoginResults.UserNotExist:
                        ModelState.AddModelError("Email", "User Not Exists.");
                        break;
                    case UserLoginResults.Deleted:
                        ModelState.AddModelError("", "Account Has Been Deleted.");
                        break;

                    case UserLoginResults.NotActive:
                        ModelState.AddModelError("", "Account Has Been Suspended.");
                        break;

                    default:
                        ModelState.AddModelError("Password", "Invalid Credentials");
                        break;
                }
            }
            return View(model);
        }


        //     [HttpGet]
        //     public async Task<IActionResult> LogOut()
        //     {

        ////SignOutAsync is Extension method for SignOut    
        //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        ////Redirect to home page

        //var activityLog = new ActivityLog
        //{
        //	UserId = (int)Int64.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value.ToString()),
        //    CreatedOn = DateTime.Now,
        //	EntityName = "Logout",
        //	LogOutTime = DateTime.Now,
        //	// Add other properties as needed
        //};

        //// Save the activity log
        //try
        //{
        //	_dbContext.ActivityLogs.Add(activityLog);
        //	await _dbContext.SaveChangesAsync();
        //}
        //catch (Exception ex)
        //{
        //	// Log or handle the exception
        //	Console.WriteLine(ex.Message);
        //	throw; // rethrow the exception to propagate it further if needed
        //}



        //return LocalRedirect("/App/Login");
        //     }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            // Find the user ID from the claims
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == "UserId");
            if (userIdClaim != null && Int64.TryParse(userIdClaim.Value, out var userId))
            {
                // Find the corresponding login activity log
                var loginActivityLog = await _dbContext.ActivityLogs
                    .Where(log => log.UserId == userId && log.EntityName == "Login")
                    .OrderByDescending(log => log.CreatedOn)
                    .FirstOrDefaultAsync();

                if (loginActivityLog != null)
                {
                    // Update the existing login activity log with the logout time
                    loginActivityLog.LogOutTime = DateTime.Now;

                    // Save the changes
                    try
                    {
                        await _dbContext.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        // Log or handle the exception
                        Console.WriteLine(ex.Message);
                        throw; // rethrow the exception to propagate it further if needed
                    }
                }
            }

            // Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect to the login page
            return LocalRedirect("/App/Login");
        }


        [HttpGet("/Applications")]
        [Authorize]
        public IActionResult Applications()
        {
            return View();
        }

        [HttpGet("/Profile")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]
        public IActionResult Profile()
        {
            UserCreateDTO userCreateDTO;
            Int64 UserId = Int64.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value.ToString());

            var roles = _roleService.GetAllRoles();

            User user = _userService.GetUserByUserId(int.Parse(UserId.ToString()));
            userCreateDTO = new UserCreateDTO();
            userCreateDTO.UserId = user.UserId;
            userCreateDTO.FirstName = user.FirstName;
            userCreateDTO.MiddleName = user.MiddleName;
            userCreateDTO.LastName = user.LastName;
            userCreateDTO.Email = user.Email;
            userCreateDTO.PhoneNo = user.PhoneNo;

            userCreateDTO.DateOfBirth = user.DateOfBirth;
            userCreateDTO.UserTypeId = user.UserTypeId;
            userCreateDTO.roles = new List<UserRolesDTO>();
            for (int i = 0; i < roles.Count; i++)
            {
                UserRolesDTO userRolesDTO = new UserRolesDTO();
                userRolesDTO.IsSelected = false;
                for (int j = 0; j < user.UserRoleMappings.Count; j++)
                {
                    if (user.UserRoleMappings[j].RoleId == roles[i].RoleId)
                    {
                        userRolesDTO.IsSelected = true;
                    }

                }

                userRolesDTO.RoleId = roles[i].RoleId;
                userRolesDTO.RoleName = roles[i].RoleName;
                userCreateDTO.roles.Add(userRolesDTO);

            }

            ViewBag.UserTypeList = new SelectList((System.Collections.IEnumerable)_userService.GetUserTypes(), "ID", "Name");

            return View(userCreateDTO);
        }



        [HttpGet("/Forbidden")]
        public IActionResult Forbidden()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SetPassword(string token, string email)
        {
            SetPasswordModel setPasswordModel = new SetPasswordModel();
            setPasswordModel.Token = token;
            setPasswordModel.Email = email;

            return View(setPasswordModel);
        }

        [HttpPost]
        public IActionResult SetPassword(SetPasswordModel setPasswordModel)
        {
            if (ModelState.IsValid)
            {
                var a = _userService.SetPassword(setPasswordModel.Password, setPasswordModel.Email, setPasswordModel.Token);
                if (a == MessageEnum.Success)
                {
                    return RedirectToAction("PasswordSetSuccess");
                }
                else
                {
                    return RedirectToAction("PasswordSetFailure");
                }
            }
            else
            {
                return View(setPasswordModel);
            }
        }

        public IActionResult PasswordSetSuccess()
        {
            return View();
        }
        public IActionResult PasswordSetFailure()
        {
            return View();
        }




    }
}
