using CloudVOffice.Core.Domain.Pemission;
using CloudVOffice.Data.Persistence;
using CloudVOffice.Data.Repository;

namespace CloudVOffice.Services.Permissions
{
    public class UserViewPermissionService : IUserViewPermissions
    {
        private readonly ApplicationDBContext _context;
        private readonly ISqlRepository<UserWiseViewMapper> _userWiseViewMapperRepo;
        public UserViewPermissionService(ApplicationDBContext context, ISqlRepository<UserWiseViewMapper> userWiseViewMapperRepo)
        {
            _context = context;
            _userWiseViewMapperRepo = userWiseViewMapperRepo;

        }

        public string AssignViewPermissions(Int64 UserId, int RoleId)
        {
            var roleWisePermissions = _context.RoleAndApplicationWisePermissions.Where(x => x.RoleId == RoleId).ToList();
            for (int j = 0; j < roleWisePermissions.Count; j++)
            {
                var obj = _context.UserWiseViewMappers.Where(x => x.Deleted == false && x.ApplicationId == roleWisePermissions[j].ApplicationId && x.UserId == UserId).SingleOrDefault();
                if (obj == null)
                {
                    UserWiseViewMapper userWiseViewMapper = new UserWiseViewMapper();
                    userWiseViewMapper.ApplicationId = roleWisePermissions[j].ApplicationId;
                    userWiseViewMapper.UserId = UserId;
                    _userWiseViewMapperRepo.Insert(userWiseViewMapper);
                }


            }
            return "Success";
        }

        public string UnAssignViewPermissions(long UserId, int RoleId)
        {
            var roleWisePermissions = _context.RoleAndApplicationWisePermissions.Where(x => x.RoleId == RoleId).ToList();
            for (int j = 0; j < roleWisePermissions.Count; j++)
            {
                var obj = _context.UserWiseViewMappers.Where(x => x.Deleted == false && x.ApplicationId == roleWisePermissions[j].ApplicationId && x.UserId == UserId).SingleOrDefault();
                if (obj != null)
                {
                    _userWiseViewMapperRepo.Delete(obj.UserWiseViewMapperId);

                }


            }
            return "Success";
        }
    }
}
