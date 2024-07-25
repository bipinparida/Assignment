using CloudVOffice.Data.DTO.Logging;

namespace CloudVOffice.Services.Logging
{
    public interface IErrorLogService
    {
        public void LogError(ErrorLogDTO errorLog);
    }
}
