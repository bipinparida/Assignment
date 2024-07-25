using CloudVOffice.Core.Domain.Common;
using CloudVOffice.Core.Domain.Pemission;
using CloudVOffice.Core.Domain.Users;
using CloudVOffice.Data.DTO.Users;

namespace CloudVOffice.Services.Users
{
    public interface IUserService
    {

        public Task<User> GetUserByEmailAsync(string Email);
        public User GetUserByUserId(Int64 UserId);

        public Task<UserRoleMapping> GetUserMappedRolesByUserIdAsync(int UserId);

        public List<Application> GetUserMenu(Int64 UserId);
        public Task<MessageEnum> CreateUser(UserCreateDTO userCreateDTO);
        public string AssignRole(Int64 userid, int roleid);

        public string UnAssignRole(Int64 userid, int RoleId);

        public List<User> GetAllUsers();
        public object GetUserTypes();

        public MessageEnum DeleteUser(Int64 UserId, Int64 deletedby);

        public Task<MessageEnum> UpdateUser(UserCreateDTO userCreateDTO);

        public List<User> GetUsersByUserType(UserType userType);

        public void SendWelcomeMessage(User user);

        public MessageEnum SetPassword(string password, string email, string token);
        public MessageEnum SendResetPasswordEmail(string EmailId);

		public MessageEnum ChangePassword(string Password, Int64 UserId);

	}
}
