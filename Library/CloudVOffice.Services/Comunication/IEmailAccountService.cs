using CloudVOffice.Core.Domain.Common;
using CloudVOffice.Core.Domain.Comunication;
using CloudVOffice.Data.DTO.Comunication;

namespace CloudVOffice.Services.Comunication
{
    public interface IEmailAccountService
    {
        public MessageEnum EmailAccountCreate(EmailAccountDTO emailAccountDTO);
        public List<EmailAccount> GetEmailAccounts();
        public EmailAccount GetEmailAccountByEmailAccountId(int emailAccountId);
        public EmailAccount GetEmailAccountByEmailAccountName(string emailAccountName);
        public MessageEnum EmailAccountUpdate(EmailAccountDTO emailAccountDTO);
        public MessageEnum EmailAccountDelete(int emailAccountId, int DeletedBy);

        public EmailAccount GetDefaultEmail(int? AccountId);


    }
}
