using CloudVOffice.Core.Domain.Pemission;
using CloudVOffice.Data.DTO.Permission;

namespace CloudVOffice.Services.Applications
{
    public interface IApplicationInstallationService
    {
        public InstalledApplication InstallApplication(string PackageName, Int64 CreatedBy, double Version);
        public List<InstalledApplication> GetInstalledApplications();

        public InstalledApplication UnInstallApplication(string PackageName, Int64 UpdatedBy);

        public Task<Application> CreateApplication(ApplicationDTO applicationDTO);

        public Task<RoleAndApplicationWisePermission> CreateRoleAndApplicationWisePermission(int RoleId, int ApplicationId, Int64 CreatedBy);
    }
}
