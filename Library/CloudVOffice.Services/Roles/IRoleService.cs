using CloudVOffice.Core.Domain.Users;

namespace CloudVOffice.Services.Roles
{
    public interface IRoleService
    {
        public Task<Role> CreateRole(string RoleName, Int64 CreatedBy);
        public List<Role> GetAllRoles();
        public Role GetRoleById(string RoleId);
        public Role GetRoleByName(string RoleName);
        public string UpdateRole(string RoleName, int RoleId, int UpdatedBy);
        public string DeleteRole(int RoleId, int DeletedBy);
    }
}
