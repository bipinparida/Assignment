using CloudVOffice.Core.Domain.Users;
using CloudVOffice.Data.Persistence;
using CloudVOffice.Data.Repository;

namespace CloudVOffice.Services.Roles
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDBContext _context;
        private readonly ISqlRepository<Role> _roleRepo;
        public RoleService(ApplicationDBContext context, ISqlRepository<Role> roleRepo)
        {
            _context = context; ;
            _roleRepo = roleRepo;
        }
        public async Task<Role> CreateRole(string RoleName, Int64 CreatedBy)
        {
            var role = _context.Roles.Where(x => x.RoleName == RoleName && x.Deleted == false).FirstOrDefault();
            if (role == null)
            {
                role = _roleRepo.Insert(new Role { RoleName = RoleName, CreatedBy = CreatedBy, CreatedDate = System.DateTime.Now, Deleted = false });

            }
            return role;
        }

        public string DeleteRole(int RoleId, int DeletedBy)
        {
            throw new NotImplementedException();
        }

        public List<Role> GetAllRoles()
        {
            return _context.Roles.Where(x => x.Deleted == false).ToList();
        }

        public Role GetRoleById(string RoleId)
        {
            throw new NotImplementedException();
        }

        public Role GetRoleByName(string RoleName)
        {
            throw new NotImplementedException();
        }

        public string UpdateRole(string RoleName, int RoleId, int UpdatedBy)
        {
            throw new NotImplementedException();
        }
    }
}
