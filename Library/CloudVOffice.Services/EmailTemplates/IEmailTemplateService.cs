using CloudVOffice.Core.Domain.EmailTemplates;

namespace CloudVOffice.Services.EmailTemplates
{
    public interface IEmailTemplateService
    {
        public EmailTemplate GetEmailTemplateByName(string name);
    }
}
