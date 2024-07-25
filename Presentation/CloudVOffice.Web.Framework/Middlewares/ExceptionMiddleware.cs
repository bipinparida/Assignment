using CloudVOffice.Web.Framework.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CloudVOffice.Web.Framework.Middlewares
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if(contextFeature != null)
                    {
                        if (contextFeature.Error.GetType().Name.Equals("ArgumentException"))
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        }
                        logger.Here().Error($"Error Message: {contextFeature.Error}");

                        
                    }
                });
            });
        }
    }
}
