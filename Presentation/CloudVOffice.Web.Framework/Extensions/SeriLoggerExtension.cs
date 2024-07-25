using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CloudVOffice.Web.Framework.Extensions
{
    public static class SeriLoggerExtension
    {
        public static ILogger Here(this ILogger logger, 
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            ) {

            return logger
                    .ForContext("membername", memberName)
                    .ForContext("filepath", sourceFilePath)
                    .ForContext("linenumber", sourceLineNumber);
        
        }
    }
}
