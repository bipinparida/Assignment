namespace CloudVOffice.Core.Domain.Logging
{
    public class ErrorLog
    {
        public Int64 ErrorLogId { get; set; }
        public DateTime LogedOn { get; set; }
        public Int64? UserId { get; set; }
        public int StatusCode { get; set; }
        public string? AreaName { get; set; }
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
        public string StackTrace { get; set; }
        public string ErrorMessage { get; set; }
    }
}
