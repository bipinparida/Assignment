using MailKit.Net.Smtp;
using MimeKit;


namespace CloudVOffice.Services.Email
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(mailRequest.SenderEmail);
            email.From.Add(new MailboxAddress(mailRequest.MailBoxName, mailRequest.MailBoxEmail));
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();

            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(mailRequest.Host, mailRequest.Port);
            smtp.Authenticate(mailRequest.AuthEmail, mailRequest.AuthPassword);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }


    }
}
