using CloudVOffice.Services.Applications;

namespace CloudVOffice.BackgroundJobs
{
    public class ScheduledService
    {
        private readonly IApplicationInstallationService _applicationInstallationService;
        public ScheduledService(IApplicationInstallationService applicationInstallationService
            )
        {
            _applicationInstallationService = applicationInstallationService;
           
        }
        public async Task RunDaily1015AmISTJob()
        {
            
        }
        public async Task RunDaily8AmISTJob()
        {
           
        }
    }
}
