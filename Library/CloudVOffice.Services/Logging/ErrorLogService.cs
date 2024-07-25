using CloudVOffice.Core.Domain.Logging;
using CloudVOffice.Data.DTO.Logging;
using CloudVOffice.Data.Persistence;
using CloudVOffice.Data.Repository;

namespace CloudVOffice.Services.Logging
{
    public class ErrorLogService : IErrorLogService
    {
        private readonly ApplicationDBContext _Context;
        private readonly ISqlRepository<ErrorLog> _errorLogRepo;
        public ErrorLogService(ApplicationDBContext Context, ISqlRepository<ErrorLog> errorLogRepo)
        {

            _Context = Context;
            _errorLogRepo = errorLogRepo;
        }
        public void LogError(ErrorLogDTO errorLog)
        {
            try
            {
                _errorLogRepo.Insert(new ErrorLog
                {
                    LogedOn = errorLog.LogedOn,
                    UserId = errorLog.UserId,
                    StatusCode = errorLog.StatusCode,
                    AreaName = errorLog.AreaName,
                    ControllerName = errorLog.ControllerName,
                    ActionName = errorLog.ActionName,
                    StackTrace = errorLog.StackTrace,
                    ErrorMessage = errorLog.ErrorMessage,
                });
            }
            catch
            {
                throw;
            }
        }
    }
}
