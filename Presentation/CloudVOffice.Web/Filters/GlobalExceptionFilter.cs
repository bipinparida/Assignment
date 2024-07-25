using CloudVOffice.Data.DTO.Logging;
using CloudVOffice.Services.Logging;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CloudVOffice.Web.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;
        private readonly IErrorLogService _errorLogService;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger,
            IErrorLogService errorLogService
            )
        {
            _logger = logger;
            _errorLogService = errorLogService;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "An exception occurred: {ErrorMessage}", context.Exception.Message);

            // You can perform additional actions here if needed, such as returning a specific response or redirecting.

            // Mark the exception as handled
            if (context.HttpContext.User.Identity.IsAuthenticated == true)
            {
                Int64 userid = Int64.Parse(context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId").Value.ToString());

                _errorLogService.LogError(new ErrorLogDTO
                {
                    LogedOn = DateTime.Now,
                    UserId = userid,
                    StatusCode = context.HttpContext.Response.StatusCode,
                    AreaName = (string)context.RouteData.DataTokens["area"],
                    ControllerName = context.HttpContext.Request.Path,
                    ActionName = context.ActionDescriptor.DisplayName,
                    StackTrace = context.Exception.StackTrace,
                    ErrorMessage = context.Exception.Message


                });
            }
            else
            {
                _errorLogService.LogError(new ErrorLogDTO
                {
                    LogedOn = DateTime.Now,
                    StatusCode = context.HttpContext.Response.StatusCode,

                    AreaName = (string)context.RouteData.DataTokens["area"],
                    ControllerName = context.HttpContext.Request.Path,
                    ActionName = context.ActionDescriptor.DisplayName,
                    StackTrace = context.Exception.StackTrace,
                    ErrorMessage = context.Exception.Message


                });


            }

            context.ExceptionHandled = true;
            if (context.HttpContext.Response.StatusCode == 400)
            {

            }
            else if (context.HttpContext.Response.StatusCode == 401)
            {

            }
            else if (context.HttpContext.Response.StatusCode == 402)
            {

            }
            else if (context.HttpContext.Response.StatusCode == 403)
            {

            }
            else if (context.HttpContext.Response.StatusCode == 403)
            {

            }
            else if (context.HttpContext.Response.StatusCode == 404)
            {

            }


        }
    }
}
