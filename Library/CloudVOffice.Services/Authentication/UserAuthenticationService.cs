using CloudVOffice.Core.Domain.Users;
using CloudVOffice.Core.Security;
using CloudVOffice.Data.Persistence;
using CloudVOffice.Data.Repository;
using CloudVOffice.Services.Users;

namespace CloudVOffice.Services.Authentication
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly ApplicationDBContext _context;
        private readonly ISqlRepository<User> _userRepo;
        private readonly IUserService _userService;
        public UserAuthenticationService(ApplicationDBContext context,
                                         ISqlRepository<User> userRepo, 
                                         IUserService userService
                                        )
        {
            _context = context;
            _userRepo = userRepo;
            _userService = userService;
        }


        public async Task<UserLoginResults> ValidateUserAsync(string EmailId, string Password)
        {
            var user = await _userService.GetUserByEmailAsync(EmailId);
            if (user == null)
                return UserLoginResults.UserNotExist;
            if (user.Deleted)
                return UserLoginResults.Deleted;
            if (!user.IsActive)
                return UserLoginResults.NotActive;
            if (user.Password != Encrypt.EncryptPassword(Password, EmailId))
                return UserLoginResults.WrongPassword;

            user.FailedLoginAttempts = 0;
            user.LastLoginDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return UserLoginResults.Successful;
        }
    }
}
