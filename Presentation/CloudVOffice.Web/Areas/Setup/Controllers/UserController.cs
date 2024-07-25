using CloudVOffice.Core.Domain.Common;
using CloudVOffice.Core.Domain.Users;
using CloudVOffice.Data.DTO.Users;
using CloudVOffice.Services.Roles;
using CloudVOffice.Services.Users;
using CloudVOffice.Web.Framework;
using CloudVOffice.Web.Model.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudVOffice.Web.Areas.Setup.Controllers
{
    [Area(AreaNames.Setup)]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public UserController(IUserService userService,
            IRoleService roleService
            )
        {

            _userService = userService;
            _roleService = roleService;

        }
        [Authorize(Roles = "Administrator")]
        public IActionResult UserList()
        {
            var a = _userService.GetAllUsers();
            ViewBag.UserList = a;
            return View();
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateUser(Int64? UserId)
        {

            UserCreateDTO userCreateDTO;

            var roles = _roleService.GetAllRoles();

            if (UserId == null)
            {
                userCreateDTO = new UserCreateDTO();
                userCreateDTO.roles = new List<UserRolesDTO>();
                for (int i = 0; i < roles.Count; i++)
                {
                    UserRolesDTO userRolesDTO = new UserRolesDTO();

                    userRolesDTO.IsSelected = false;
                    userRolesDTO.RoleId = roles[i].RoleId;
                    userRolesDTO.RoleName = roles[i].RoleName;
                    userCreateDTO.roles.Add(userRolesDTO);

                }
            }
            else
            {
                User user = _userService.GetUserByUserId(int.Parse(UserId.ToString()));
                userCreateDTO = new UserCreateDTO();
                userCreateDTO.UserId = user.UserId;
                userCreateDTO.FirstName = user.FirstName;
                userCreateDTO.MiddleName = user.MiddleName;
                userCreateDTO.LastName = user.LastName;
                userCreateDTO.Email = user.Email;
                userCreateDTO.PhoneNo = user.PhoneNo;
                userCreateDTO.Password = user.Password;

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

            }


            ViewBag.UserTypeList = new SelectList((System.Collections.IEnumerable)_userService.GetUserTypes(), "ID", "Name");





            return View(userCreateDTO);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateUser(UserCreateDTO createUserDTO)
        {

            createUserDTO.CreatedBy = Int64.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value.ToString());
            createUserDTO.UserTypeId = 1;
            if (ModelState.IsValid)
            {
                if (createUserDTO.UserId != null)
                {
                    var a = await _userService.UpdateUser(createUserDTO);
                    if (a != null)
                    {
                        if (a == MessageEnum.Updated)
                        {
                            TempData["msg"] = MessageEnum.Updated;

                            return Redirect("/Setup/User/UserList");
                        }
                        else if (a == MessageEnum.Duplicate)
                        {
                            TempData["msg"] = MessageEnum.Duplicate;
                            ModelState.AddModelError("", "User Already Exists");
                        }
                        else
                        {
                            TempData["msg"] = MessageEnum.UnExpectedError;
                            ModelState.AddModelError("", "Un-Expected Error");
                        }

                    }
                    else
                    {
                        TempData["msg"] = MessageEnum.UnExpectedError;
                        ModelState.AddModelError("", "Un-Expected Error");
                    }
                }
                else
                {
                    var a = await _userService.CreateUser(createUserDTO);
                    if (a != null)
                    {
                        if (a == MessageEnum.Success)
                        {
                            TempData["msg"] = MessageEnum.Success;
                            if (createUserDTO.WantToStayOnThisPage)
                            {
                                return Redirect("/Setup/User/CreateUser");
                            }
                            else
                                return Redirect("/Setup/User/UserList");
                        }
                        else if (a == MessageEnum.Duplicate)
                        {
                            TempData["msg"] = MessageEnum.Duplicate;
                            ModelState.AddModelError("", "User Already Exists");
                        }
                        else
                        {
                            TempData["msg"] = MessageEnum.UnExpectedError;
                            ModelState.AddModelError("", "Un-Expected Error");
                        }

                    }
                    else
                    {
                        TempData["msg"] = MessageEnum.UnExpectedError;
                        ModelState.AddModelError("", "Un-Expected Error");
                    }
                }

            }
            ViewBag.UserTypeList = new SelectList((System.Collections.IEnumerable)_userService.GetUserTypes(), "ID", "Name");

            return View(createUserDTO);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteUser(Int64 UserId)
        {
            Int64 DeletedBy = Int64.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value.ToString());

            var a = _userService.DeleteUser(UserId, DeletedBy);
            TempData["msg"] = a;
            return Redirect("/Setup/User/UserList");
        }

		[HttpGet]
		[Authorize]
		public IActionResult ChangePassword()
        {
			ChangePasswordModel changePasswordModel = new ChangePasswordModel();

			return View(changePasswordModel);
		}


		[HttpPost]
		[Authorize]
		public IActionResult ChangePassword(ChangePasswordModel changePasswordModel)
		{
			if (ModelState.IsValid)
			{
				Int64 UserId = Int64.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value.ToString());

				MessageEnum messageEnum = _userService.ChangePassword(changePasswordModel.Password, UserId);
				TempData["msg"] = messageEnum;
				return Redirect("/Setup/User/ChangePassword");
			}

			return View(changePasswordModel);
		}



		//      public IActionResult Profile(Int64? UserId)
		//      {
		//	UserCreateDTO userCreateDTO;

		//	var roles = _roleService.GetAllRoles();

		//	if (UserId == null)
		//	{
		//		userCreateDTO = new UserCreateDTO();
		//		userCreateDTO.roles = new List<UserRolesDTO>();
		//		for (int i = 0; i < roles.Count; i++)
		//		{
		//			UserRolesDTO userRolesDTO = new UserRolesDTO();

		//			userRolesDTO.IsSelected = false;
		//			userRolesDTO.RoleId = roles[i].RoleId;
		//			userRolesDTO.RoleName = roles[i].RoleName;
		//			userCreateDTO.roles.Add(userRolesDTO);
		//		}
		//	}
		//	else
		//	{
		//		User user = _userService.GetUserByUserId(int.Parse(UserId.ToString()));
		//		userCreateDTO = new UserCreateDTO();
		//		userCreateDTO.UserId = user.UserId;
		//		userCreateDTO.FirstName = user.FirstName;
		//		userCreateDTO.MiddleName = user.MiddleName;
		//		userCreateDTO.LastName = user.LastName;
		//		userCreateDTO.Email = user.Email;
		//		userCreateDTO.PhoneNo = user.PhoneNo;
		//		userCreateDTO.Password = user.Password;

		//		userCreateDTO.DateOfBirth = user.DateOfBirth;
		//		userCreateDTO.UserTypeId = user.UserTypeId;
		//		userCreateDTO.roles = new List<UserRolesDTO>();
		//		for (int i = 0; i < roles.Count; i++)
		//		{
		//			UserRolesDTO userRolesDTO = new UserRolesDTO();
		//			userRolesDTO.IsSelected = false;
		//			for (int j = 0; j < user.UserRoleMappings.Count; j++)
		//			{
		//				if (user.UserRoleMappings[j].RoleId == roles[i].RoleId)
		//				{
		//					userRolesDTO.IsSelected = true;
		//				}

		//			}

		//			userRolesDTO.RoleId = roles[i].RoleId;
		//			userRolesDTO.RoleName = roles[i].RoleName;
		//			userCreateDTO.roles.Add(userRolesDTO);

		//		}

		//	}

		//	ViewBag.UserTypeList = new SelectList((System.Collections.IEnumerable)_userService.GetUserTypes(), "ID", "Name");

		//	return View(userCreateDTO);
		//}




		//[HttpPost]
		//public async Task<IActionResult> Profile(UserCreateDTO createUserDTO)
		//{

		//	createUserDTO.CreatedBy = Int64.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value.ToString());
		//	createUserDTO.UserTypeId = 1;
		//	if (ModelState.IsValid)
		//	{
		//		if (createUserDTO.UserId != null)
		//		{
		//			var a = await _userService.UpdateUser(createUserDTO);
		//			if (a != null)
		//			{
		//				if (a == MessageEnum.Updated)
		//				{
		//					TempData["msg"] = MessageEnum.Updated;

		//					return Redirect("/Applications");
		//				}
		//			}
		//			else
		//			{
		//				TempData["msg"] = MessageEnum.UnExpectedError;
		//				ModelState.AddModelError("", "Un-Expected Error");
		//			}
		//		}


		//	}
		//	ViewBag.UserTypeList = new SelectList((System.Collections.IEnumerable)_userService.GetUserTypes(), "ID", "Name");

		//	return View(createUserDTO);
		//}

	}
}
