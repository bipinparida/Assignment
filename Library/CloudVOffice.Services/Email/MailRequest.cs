namespace CloudVOffice.Services.Email
{
    public class MailRequest
    {
        public string SenderEmail { get; set; }
        public string MailBoxName { get; set; }
        public string MailBoxEmail { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

        public string AuthEmail { get; set; }
        public string AuthPassword { get; set; }

        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
