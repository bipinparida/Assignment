using CloudVOffice.Core.Domain.Pemission;
using CloudVOffice.Data.DTO.Permission;
using CloudVOffice.Data.Persistence;
using CloudVOffice.Data.Repository;
using Pipelines.Sockets.Unofficial.Arenas;

namespace CloudVOffice.Services.Applications
{
    public class ApplicationInstallationService : IApplicationInstallationService
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly ISqlRepository<InstalledApplication> _installedApplicationRepo;
        private readonly ISqlRepository<RoleAndApplicationWisePermission> _roleAndApplicationWisePermissionRepo;
        private readonly ISqlRepository<Application> _applicationRepo;
        public ApplicationInstallationService(ApplicationDBContext dbContext, ISqlRepository<InstalledApplication> installedApplicationRepo, ISqlRepository<RoleAndApplicationWisePermission> roleAndApplicationWisePermissionRepo,
            ISqlRepository<Application> applicationRepo
            )
        {
            _dbContext = dbContext;
            _installedApplicationRepo = installedApplicationRepo;
            _roleAndApplicationWisePermissionRepo = roleAndApplicationWisePermissionRepo;
            _applicationRepo = applicationRepo;


        }

        public async Task<Application> CreateApplication(ApplicationDTO applicationDTO)
        {
            var a = _dbContext.Applications.Where(x => x.ApplicationName == applicationDTO.ApplicationName && x.Deleted == false && x.AreaName == applicationDTO.AreaName && x.Url == applicationDTO.Url).FirstOrDefault();
            if (a == null)
            {
                a = _applicationRepo.Insert(new Application
                {
                    ApplicationName = applicationDTO.ApplicationName,
                    Parent = applicationDTO.Parent,
                    IsGroup = applicationDTO.IsGroup,
                    Url = applicationDTO.Url,
                    CreatedBy = applicationDTO.CreatedBy,
                    CreatedDate = System.DateTime.Now,
                    Deleted = false,
                    IconImageUrl = applicationDTO.IconImageUrl,
                    IconClass = applicationDTO.IconClass,
                    AreaName = applicationDTO.AreaName,


                });
            }
            return a;
        }

        public async Task<RoleAndApplicationWisePermission> CreateRoleAndApplicationWisePermission(int RoleId, int ApplicationId, Int64 CreatedBy)
        {
            var roleapp = _dbContext.RoleAndApplicationWisePermissions.Where(x => x.RoleId == RoleId && x.ApplicationId == ApplicationId && x.Deleted == false).FirstOrDefault();
            if (roleapp == null)
            {
                return _roleAndApplicationWisePermissionRepo.Insert(new RoleAndApplicationWisePermission()
                {
                    RoleId = RoleId,
                    ApplicationId = ApplicationId,
                    CreatedBy = CreatedBy,
                    CreatedDate = System.DateTime.Now,
                    Deleted = false
                });


            }
            else return roleapp;
        }

        public List<InstalledApplication> GetInstalledApplications()
        {
            return _dbContext.InstalledApplications.Where(x => x.Deleted == false).ToList();
        }

        public InstalledApplication InstallApplication(string PackageName, Int64 CreatedBy, double v)
        {
            var obj = _dbContext.InstalledApplications.Where(x => x.PackageName == PackageName && x.Deleted == false && x.Version == v).FirstOrDefault();
            if (obj == null)
            {
                InstalledApplication installedApplication = new InstalledApplication();
                installedApplication.PackageName = PackageName;
                installedApplication.Version = v;
                installedApplication.Deleted = false;
                installedApplication.CreatedDate = DateTime.Now;
                installedApplication.CreatedBy = CreatedBy;
                var o = _installedApplicationRepo.Insert(installedApplication);
                return o;
            }
            else
            {
                return obj;
            }
        }

        public InstalledApplication UnInstallApplication(string PackageName, long UpdatedBy)
        {
            var obj = _dbContext.InstalledApplications.Where(x => x.PackageName == PackageName && x.Deleted == false).FirstOrDefault();
            obj.Deleted = true;
            obj.UpdatedDate = DateTime.Now;
            obj.UpdatedBy = UpdatedBy;
            _dbContext.SaveChanges();
            return obj;
        }
    }
}
