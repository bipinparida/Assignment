using CloudVOffice.Core.Domain.Common;
using CloudVOffice.Core.Domain.Company;
using CloudVOffice.Core.Domain.Comunication;
using CloudVOffice.Core.Domain.EmailTemplates;
using CloudVOffice.Core.Domain.Pemission;
using CloudVOffice.Core.Domain.Users;
using CloudVOffice.Core.Security;
using CloudVOffice.Data.DTO.Users;
using CloudVOffice.Data.Persistence;
using CloudVOffice.Data.Repository;
using CloudVOffice.Services.Company;
using CloudVOffice.Services.Comunication;
using CloudVOffice.Services.Email;
using CloudVOffice.Services.EmailTemplates;
using CloudVOffice.Services.Permissions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text;


namespace CloudVOffice.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ApplicationDBContext _context;
        private readonly ISqlRepository<User> _userRepo;
        private readonly ISqlRepository<UserRoleMapping> _userrolemappingRepo;
        private readonly ISqlRepository<UserWiseViewMapper> _userViewmappingRepo;
        private readonly IUserViewPermissions _userViewPermissions;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICompanyDetailsService _companyDetailsService;
        private readonly ILetterHeadService _letterHeadService;
        private readonly IEmailAccountService _emailAccountService;
        private readonly IEmailService _emailService;
        public UserService(ApplicationDBContext context,
            ISqlRepository<User> userRepo,
            ISqlRepository<UserRoleMapping> userrolemappingRepo,
            IUserViewPermissions userViewPermissions,
             IEmailTemplateService emailTemplateService,
             IHttpContextAccessor httpContextAccessor,
             ICompanyDetailsService companyDetailsService,
             ILetterHeadService letterHeadService,
             IEmailAccountService emailAccountService,
        IEmailService emailService
             )
        {
            _context = context;
            _userRepo = userRepo;
            _userrolemappingRepo = userrolemappingRepo;
            _userViewPermissions = userViewPermissions;
            _emailTemplateService = emailTemplateService;
            _httpContextAccessor = httpContextAccessor;
            _letterHeadService = letterHeadService;
            _companyDetailsService = companyDetailsService;
            _emailAccountService = emailAccountService;
            _emailService = emailService;
        }


        public async Task<User> GetUserByEmailAsync(string Email)
        {
            var user = _context.Users.Include(s => s.UserRoleMappings).ThenInclude(a => a.Role)

                .Where(x => x.Email == Email).SingleOrDefault();

            return user;
        }

        public User GetUserByUserId(Int64 UserId)
        {
            return _context.Users
                  .Include(x => x.UserRoleMappings)
                .Where(x => x.Deleted == false && x.UserId == UserId).SingleOrDefault();
        }

        public Task<UserRoleMapping> GetUserMappedRolesByUserIdAsync(int UserId)
        {
            throw new NotImplementedException();
        }

		//public List<Application> GetUserMenu(Int64 UserId)
		//{
		//    var user = _context.Users.Include(s => s.UserRoleMappings).ThenInclude(a => a.Role)

		//     .Where(x => x.UserId == UserId).SingleOrDefault();
		//    List<Application> application = new List<Application>();
		//    for (int i = 0; i < user.UserRoleMappings.Count; i++)
		//    {
		//        var pmenu = _context.UserWiseViewMappers
		//            .Include(a => a.Application)
		//            .Where(x => x.UserId == UserId && x.Deleted == false && x.Application.Parent == null).ToList();
		//        for (int j = 0; j < pmenu.Count; j++)
		//        {
		//            if (application.Where(x => x.ApplicationId == pmenu[j].ApplicationId).ToList().Count == 0)
		//            {
		//                pmenu[j].Application.Children = new List<Application>();

		//                application.Add(pmenu[j].Application);
		//            }
		//        }
		//        for (int j = 0; j < application.Count; j++)
		//        {
		//            var smenu = _context.UserWiseViewMappers
		//                .Include(a => a.Application)
		//                .Where(x => x.UserId == UserId && x.Deleted == false && x.Application.Parent == application[j].ApplicationId).ToList();
		//            for (int k = 0; k < smenu.Count; k++)
		//            {
		//                var slist = application[j].Children;
		//                if (application[j].Children != null && application[j].Children.Count > 0)
		//                {
		//                    int sapplicationId = smenu[k].ApplicationId;

		//                    if (slist.Where(x => x.ApplicationId == sapplicationId).ToList().Count == 0)
		//                    {
		//                        smenu[k].Application.Children = new List<Application>();
		//                        application[j].Children.Add(smenu[k].Application);
		//                    }
		//                }
		//                else
		//                {
		//                    smenu[k].Application.Children = new List<Application>();

		//                    application[j].Children.Add(smenu[k].Application);
		//                }

		//                var tmenu = _context.UserWiseViewMappers
		//                    .Include(a => a.Application)
		//                    .Where(x => x.UserId == UserId && x.Deleted == false && x.Application.Parent == application[j].Children[k].ApplicationId).ToList();
		//                for (int l = 0; l < tmenu.Count; l++)
		//                {
		//                    var tlist = application[j].Children[k].Children;
		//                    if (tlist != null && tlist.Count > 0)
		//                    {
		//                        int tapplicationId = tmenu[l].ApplicationId;

		//                        if (tlist.Where(x => x.ApplicationId == tmenu[l].ApplicationId).ToList().Count == 0)
		//                        {
		//                            var napplication = tmenu[l].Application;
		//                            application[j].Children[k].Children.Add(napplication);
		//                        }
		//                    }
		//                    else
		//                    {
		//                        var napplication = tmenu[l].Application;
		//                        application[j].Children[k].Children.Add(napplication);
		//                    }

		//                }
		//            }
		//        }

		//    }
		//    return application;

		//}
		public List<Application> GetUserMenu(Int64 UserId)
		{
			var user = _context.Users.Include(s => s.UserRoleMappings).ThenInclude(a => a.Role)

			 .Where(x => x.UserId == UserId).SingleOrDefault();

			List<int> roles = new List<int>();
			for (int i = 0; i < user.UserRoleMappings.Count; i++)
			{
				roles.Add(user.UserRoleMappings[i].RoleId);
			}

			List<Application> application = new List<Application>();

			var pmenu = _context.RoleAndApplicationWisePermissions
			   .Include(a => a.Application)
			   .Where(x => roles.Contains(x.RoleId) && x.Deleted == false && x.Application.Parent == null).ToList();


			for (int j = 0, pc = 0; j < pmenu.Count; j++)
			{
				if (application.Where(x => x.ApplicationId == pmenu[j].ApplicationId).ToList().Count == 0)
				{
					pmenu[j].Application.Children = new List<Application>();

					application.Add(pmenu[j].Application);

					var smenu = _context.RoleAndApplicationWisePermissions
					.Include(a => a.Application)
					.Where(x => roles.Contains(x.RoleId) && x.Deleted == false && x.Application.Parent == pmenu[j].ApplicationId).ToList();

					for (int k = 0, sc = 0; k < smenu.Count; k++)
					{
						int ApplicationId = smenu[k].ApplicationId;
						var appli = application[pc];
						var Sappcheck = appli.Children.Where(x => x.ApplicationId == ApplicationId).ToList();
						if (Sappcheck.Count == 0)
						{
							smenu[k].Application.Children = new List<Application>();
							application[pc].Children.Add(smenu[k].Application);
							var tmenu = _context.RoleAndApplicationWisePermissions
							.Include(a => a.Application)
							.Where(x => roles.Contains(x.RoleId) && x.Deleted == false && x.Application.Parent == smenu[k].ApplicationId).ToList();

							for (int l = 0; l < tmenu.Count; l++)
							{
								var tappcheck = application[pc].Children[sc].Children.Where(x => x.ApplicationId == tmenu[l].ApplicationId).ToList();
								if (tappcheck.Count == 0)
								{
									tmenu[l].Application.Children = new List<Application>();
									application[pc].Children[sc].Children.Add(tmenu[l].Application);
								}

							}
							sc = sc + 1;
						}



					}
					pc = pc + 1;

				}


			}

			return application;

		}
		public async Task<MessageEnum> CreateUser(UserCreateDTO userCreateDTO)
        {
            var objCheck = _context.Users

                .SingleOrDefault(opt => opt.Email == userCreateDTO.Email && opt.Deleted == false);
            try
            {
                if (objCheck == null)
                {

                    User users = new User();
                    users.FirstName = userCreateDTO.FirstName;
                    users.MiddleName = userCreateDTO.MiddleName;
                    users.LastName = userCreateDTO.LastName;
                    users.Email = userCreateDTO.Email;
                    users.Password = Encrypt.EncryptPassword(userCreateDTO.Password, userCreateDTO.Email) ;
                    users.PhoneNo = userCreateDTO.PhoneNo;
                    users.DateOfBirth = userCreateDTO.DateOfBirth;
                    users.UserTypeId = userCreateDTO.UserTypeId;
                    users.IsActive = true;
                    users.CreatedBy = userCreateDTO.CreatedBy;
                    var obj = _userRepo.Insert(users);
                    for (int i = 0; i < userCreateDTO.roles.Count; i++)
                    {
                        if (userCreateDTO.roles[i].IsSelected == true)
                        {
                            AssignRole(obj.UserId, userCreateDTO.roles[i].RoleId);
                           // _userViewPermissions.AssignViewPermissions(obj.UserId, userCreateDTO.roles[i].RoleId);
                        }

                    }
                  //  SendWelcomeMessage(obj);

                    return MessageEnum.Success;

                }
                else if (objCheck != null)
                {
                    return MessageEnum.Duplicate;
                }

                return MessageEnum.UnExpectedError;
            }
            catch
            {
                throw;
            }
        }

        public async Task<MessageEnum> UpdateUser(UserCreateDTO userCreateDTO)
        {
            var user = _context.Users
                .Include(x => x.UserRoleMappings)
                //.Include(x => x.UserWiseViewMapper)
                .SingleOrDefault(opt => opt.UserId == userCreateDTO.UserId && opt.Deleted == false);
            if (user != null)
            {
                user.FirstName = userCreateDTO.FirstName;
                user.MiddleName = userCreateDTO.MiddleName;
                user.LastName = userCreateDTO.LastName;
				////user.Password = Encrypt.EncryptPassword(userCreateDTO.Password, userCreateDTO.Email);
				//user.Password = userCreateDTO.Password;
				if (user.Password != userCreateDTO.Password)
				{
					user.Password = Encrypt.EncryptPassword(userCreateDTO.Password, userCreateDTO.Email);
				}
				else
				{
					user.Password = userCreateDTO.Password;
				}
				user.DateOfBirth = userCreateDTO.DateOfBirth;
                user.PhoneNo = userCreateDTO.PhoneNo;
                user.UserTypeId = userCreateDTO.UserTypeId;
                user.UpdatedBy = userCreateDTO.CreatedBy;
                user.UpdatedDate = DateTime.Now;
                _context.SaveChanges();
                var userAssignedRoles = user.UserRoleMappings.ToList();
                var userRoles = userCreateDTO.roles.Where(x => x.IsSelected == true).ToList();
                var UnAsignedRoles = userAssignedRoles.Where(x => !userRoles.Any(a => a.RoleId == x.RoleId)).ToList();
                for (int i = 0; i < UnAsignedRoles.Count; i++)
                {
                    UnAssignRole(user.UserId, UnAsignedRoles[i].RoleId);
                  //  _userViewPermissions.UnAssignViewPermissions(user.UserId, userCreateDTO.roles[i].RoleId);

                }
                for (int i = 0; i < userCreateDTO.roles.Count; i++)
                {
                    if (userCreateDTO.roles[i].IsSelected == true)
                    {
                        AssignRole(user.UserId, userCreateDTO.roles[i].RoleId);
                      //  _userViewPermissions.AssignViewPermissions(user.UserId, userCreateDTO.roles[i].RoleId);
                    }

                }
                return MessageEnum.Updated;
            }
            else
                return MessageEnum.Invalid;
        }
        public string AssignRole(Int64 userid, int roleid)
        {
            var objCheck = _context.UserRoleMappings.SingleOrDefault(opt => opt.RoleId == roleid && opt.UserId == userid);
            try
            {
                if (objCheck == null)
                {

                    UserRoleMapping userrolemapping = new UserRoleMapping();
                    userrolemapping.RoleId = roleid;
                    userrolemapping.UserId = userid;
                    var obj = _userrolemappingRepo.Insert(userrolemapping);
                    return "Role Sucessfully Assigned";

                }
                else if (objCheck != null)
                {
                    return "Duplicate";
                }

                return "Something unexpected!";
            }
            catch (Exception ex)
            {
                return "Error!";
            }
        }

        public string UnAssignRole(Int64 userid, int roleid)
        {
            var objCheck = _context.UserRoleMappings.SingleOrDefault(opt => opt.RoleId == roleid && opt.UserId == userid);
            try
            {
                if (objCheck != null)
                {

                    _userrolemappingRepo.Delete(objCheck.UserRoleMappingId);
                    return "Role Sucessfully Un-Assigned";

                }
                else if (objCheck != null)
                {
                    return "Duplicate";
                }

                return "Something unexpected!";
            }
            catch (Exception ex)
            {
                return "Error!";
            }
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.Where(x => x.Deleted == false).ToList();
        }

        public object GetUserTypes()
        {
            var enumData = from UserType e in Enum.GetValues(typeof(UserType))
                           select new
                           {
                               ID = (int)e,
                               Name = e.ToString()
                           };
            return enumData;
        }

        public MessageEnum DeleteUser(Int64 UserId, Int64 deletedby)
        {
            try
            {
                var a = _context.Users.Where(x => x.UserId == UserId).FirstOrDefault();
                if (a != null)
                {
                    a.Deleted = true;
                    a.UpdatedBy = deletedby;
                    a.UpdatedDate = DateTime.Now;
                    _context.SaveChanges();
                    return MessageEnum.Deleted;
                }
                else
                    return MessageEnum.Invalid;
            }
            catch
            {
                throw;
            }
        }

        public List<User> GetUsersByUserType(UserType userType)
        {
            return _context.Users.Where(x => x.UserType == userType && x.Deleted == false).ToList();
        }
        private string GenerateResetToken(User user)
        {
            Guid obj = Guid.NewGuid();
            user.ResetPasswordToken = Encrypt.EncryptPassword(obj.ToString(), user.Email);
            user.ResetPasswordTokenExpirey = DateTime.Now.AddMinutes(60);
            _context.SaveChanges();
            return obj.ToString();
        }




        public async void SendWelcomeMessage(User user)
        {
            try
            {
                string token = GenerateResetToken(user);
                string baseUrl = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host;
                string url = baseUrl + "/App/SetPassword?token=" + token + "&email=" + user.Email;
                EmailTemplate emailTemplate = _emailTemplateService.GetEmailTemplateByName("WelcomeEmail");

                string emailTemp = emailTemplate.EmailTemplateDescription.Trim();
                CompanyDetails company = _companyDetailsService.GetCompanyDetails();
                LetterHead letter = _letterHeadService.GetLetter();
                EmailAccount emailA = _emailAccountService.GetDefaultEmail(emailTemplate.DefaultSendingAccount);
                StringBuilder stringBuilder = new StringBuilder(emailTemp);
                if (company != null)
                {
                    stringBuilder = stringBuilder.Replace("{%emailogo%}", "<img src='" + baseUrl + "/uploads/setup/" + company.CompanyLogo + "' height=\"40\" style=\"border:0;margin:auto auto 10px;max-height:40px;outline:none;text-align:center;text-decoration:none;width:auto\" align=\"center\" width=\"auto\" class=\"CToWUd\" data-bit=\"iit\" jslog=\"138226; u014N:xr6bB; 53:WzAsMl0.\">");

                    stringBuilder
                         = stringBuilder.Replace("{%welcometitle%}", "Welcome to " + company.CompanyName);
                }




                stringBuilder = stringBuilder.Replace("{%helloname%}", "Hello " + user.FullName + ",");
                stringBuilder = stringBuilder.Replace("{%accountcreatetionmessage%}", "A new account has been created for you at <a href='" + baseUrl + "' style=\"color:#2d95f0\" target=\"_blank\" >" + baseUrl + "</a>");

                stringBuilder = stringBuilder.Replace("{%loginidmessage%}", "Your login id is: <b><a href=\'mailto:" + user.Email + "\' target=\"_blank\">" + user.Email + "</a></b>");
                stringBuilder = stringBuilder.Replace("{%aditionalmessage%}", "Click on the link below to complete your registration and set a new password.");
                stringBuilder = stringBuilder.Replace("{%setpasswordlink%}", "<a href='" + url + "'  target=\"_blank\" rel=\"noopener noreferrer\" style=\"background-color:#e54d42; padding:10px 8px 10px 8px; text-decoration:none; color:#fff; border-radius:5px; font-size:10px\">Complete Registration</a>");
                if (emailA != null)
                {
                    stringBuilder = stringBuilder.Replace("{%emailsignature%}", emailA.EmailSignature);
                }
                stringBuilder = stringBuilder.Replace("{%copylinkfrommessage%}", "You can also copy-paste following link in your browser<br/> <a href='" + url + "'  style=\"color:#2d95f0\" target=\"_blank\" >" + url + "</a>");
                if (company != null)
                {
                    stringBuilder = stringBuilder.Replace("{%companyname%}", company.CompanyName);
                    stringBuilder = stringBuilder.Replace("{%address%}", company.AddressLine1 + ", " + company.AddressLine2 + ", " + company.City + ", " + company.State + ", " + company.Country + " - " + company.PostalCode);

                }
                if (letter != null)
                {
                    stringBuilder = stringBuilder.Replace("{%footerletterhera%}", "<img src='" + baseUrl + "/uploads/setup/" + letter.LetterHeadFooterImage + "' style='hight:" + letter.LetterHeadImageFooterHeight + "; width:" + letter.LetterHeadImageFooterWidth + "'>");
                }
                if (emailA != null)
                {
                    await _emailService.SendEmailAsync(new MailRequest
                    {
                        SenderEmail = emailA.EmailAddress,
                        MailBoxName = emailA.EmailAccountName,
                        MailBoxEmail = emailA.AlternativeEmailAddress,
                        Host = emailA.EmailDomain.OutingServer,
                        Port = emailA.EmailDomain.OutgoingPort,
                        AuthEmail = emailA.EmailAddress,
                        AuthPassword = emailA.EmailPassword,
                        ToEmail = user.Email,
                        Subject = "Welcome to " + company.CompanyName,

                        Body = stringBuilder.ToString()

                    });

                }




            }
            catch
            {
                throw;
            }
        }

        public MessageEnum SetPassword(string password, string email, string token)
        {
            try
            {
                string encToken = Encrypt.EncryptPassword(token, email);
                User user = _context.Users.Where(x => x.Email == email && x.ResetPasswordToken == encToken).FirstOrDefault();
                if (user != null)
                {
                    if (DateTime.Now <= user.ResetPasswordTokenExpirey)
                    {
                        user.Password = Encrypt.EncryptPassword(password, email);
                        user.ResetPasswordToken = "";
                        user.ResetPasswordTokenExpirey = null;
                        _context.SaveChanges();
                        return MessageEnum.Success;
                    }
                    else
                    {
                        return MessageEnum.Error;
                    }
                }
                else
                {
                    return MessageEnum.Invalid;
                }
            }
            catch
            {
                throw;
            }
        }

		public MessageEnum ChangePassword(string Password, Int64 UserId)
		{
			User user = _context.Users.Where(x => x.UserId == UserId).FirstOrDefault();
			if (user != null)
			{
				user.Password = Encrypt.EncryptPassword(Password, user.Email);
				_context.SaveChanges();
				return MessageEnum.Success;
			}
			else
				return MessageEnum.Invalid;

		}

		public MessageEnum SendResetPasswordEmail(string EmailId)
        {
            var objCheck = _context.Users

             .SingleOrDefault(opt => opt.Email == EmailId && opt.Deleted == false);
            if (objCheck != null)
            {
                if (objCheck.IsActive == false)
                {
                    return MessageEnum.InActive;
                }
                else
                {
                    SendPasswordResetEmail(objCheck);
                    return MessageEnum.Success;
                }
            }
            else
            {
                return MessageEnum.Invalid;
            }
        }
        private async Task SendPasswordResetEmail(User user)
        {
            try
            {
                string token = GenerateResetToken(user);
                string baseUrl = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host;
                string url = baseUrl + "/App/SetPassword?token=" + token + "&email=" + user.Email;
                EmailTemplate emailTemplate = _emailTemplateService.GetEmailTemplateByName("PasswordReset");

                string emailTemp = emailTemplate.EmailTemplateDescription.Trim();
                CompanyDetails company = _companyDetailsService.GetCompanyDetails();
                LetterHead letter = _letterHeadService.GetLetter();
                EmailAccount emailA = _emailAccountService.GetDefaultEmail(emailTemplate.DefaultSendingAccount);
                StringBuilder stringBuilder = new StringBuilder(emailTemp);
                if (company != null)
                {
                    stringBuilder = stringBuilder.Replace("{%emailogo%}", "<img src='" + baseUrl + "/uploads/setup/" + company.CompanyLogo + "' height=\"40\" style=\"border:0;margin:auto auto 10px;max-height:40px;outline:none;text-align:center;text-decoration:none;width:auto\" align=\"center\" width=\"auto\" class=\"CToWUd\" data-bit=\"iit\" jslog=\"138226; u014N:xr6bB; 53:WzAsMl0.\">");


                }




                stringBuilder = stringBuilder.Replace("{%helloname%}", user.FullName + ",");
                stringBuilder = stringBuilder.Replace("{%setpasswordlink%}", "<a href='" + url + "'  target=\"_blank\" rel=\"noopener noreferrer\" style=\"background-color:#e54d42; padding:10px 8px 10px 8px; text-decoration:none; color:#fff; border-radius:5px; font-size:10px\">Reset Password</a>");
                if (emailA != null)
                {
                    stringBuilder = stringBuilder.Replace("{%emailsignature%}", emailA.EmailSignature);
                }
                stringBuilder = stringBuilder.Replace("{%copylinkfrommessage%}", "You can also copy-paste following link in your browser<br/> <a href='" + url + "'  style=\"color:#2d95f0\" target=\"_blank\" >" + url + "</a>");
                if (company != null)
                {
                    stringBuilder = stringBuilder.Replace("{%companyname%}", company.CompanyName);
                    stringBuilder = stringBuilder.Replace("{%address%}", company.AddressLine1 + ", " + company.AddressLine2 + ", " + company.City + ", " + company.State + ", " + company.Country + " - " + company.PostalCode);

                }
                if (letter != null)
                {
                    stringBuilder = stringBuilder.Replace("{%footerletterhera%}", "<img src='" + baseUrl + "/uploads/setup/" + letter.LetterHeadFooterImage + "' style='hight:" + letter.LetterHeadImageFooterHeight + "; width:" + letter.LetterHeadImageFooterWidth + "'>");
                }
                if (emailA != null)
                {
                    await _emailService.SendEmailAsync(new MailRequest
                    {
                        SenderEmail = emailA.EmailAddress,
                        MailBoxName = emailA.EmailAccountName,
                        MailBoxEmail = emailA.AlternativeEmailAddress,
                        Host = emailA.EmailDomain.OutingServer,
                        Port = emailA.EmailDomain.OutgoingPort,
                        AuthEmail = emailA.EmailAddress,
                        AuthPassword = emailA.EmailPassword,
                        ToEmail = user.Email,
                        Subject = emailTemplate.Subject,

                        Body = stringBuilder.ToString()

                    });

                }




            }
            catch
            {
                throw;
            }
        }
    }
}
