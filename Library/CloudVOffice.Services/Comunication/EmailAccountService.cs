using CloudVOffice.Core.Domain.Common;
using CloudVOffice.Core.Domain.Comunication;
using CloudVOffice.Data.DTO.Comunication;
using CloudVOffice.Data.Persistence;
using CloudVOffice.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Pipelines.Sockets.Unofficial.Arenas;

namespace CloudVOffice.Services.Comunication
{
    public class EmailAccountService : IEmailAccountService
    {
        private readonly ApplicationDBContext _Context;
        private readonly ISqlRepository<EmailAccount> _emailAccountRepo;
        public EmailAccountService(ApplicationDBContext Context, ISqlRepository<EmailAccount> emailAccountRepo)
        {

            _Context = Context;
            _emailAccountRepo = emailAccountRepo;
        }
        public MessageEnum EmailAccountCreate(EmailAccountDTO emailAccountDTO)
        {
            var objCheck = _Context.EmailAccounts.SingleOrDefault(opt => opt.EmailAccountId == emailAccountDTO.EmailAccountId && opt.Deleted == false);
            try
            {
                if (objCheck == null)
                {

                    EmailAccount emailAccount = new EmailAccount();
                    emailAccount.EmailAddress = emailAccountDTO.EmailAddress;
                    emailAccount.Domain = emailAccountDTO.Domain;
                    emailAccount.EmailAccountName = emailAccountDTO.EmailAccountName;
                    emailAccount.EmailPassword = emailAccountDTO.EmailPassword;
                    emailAccount.AlternativeEmailAddress = emailAccountDTO.AlternativeEmailAddress;
                    emailAccount.EmailSignature = emailAccountDTO.EmailSignature;
                    emailAccount.EmailLogo = emailAccountDTO.EmailLogo;
                    emailAccount.IsDefaultSending = emailAccountDTO.IsDefaultSending;
                    emailAccount.CreatedBy = emailAccountDTO.CreatedBy;
                    var obj = _emailAccountRepo.Insert(emailAccount);

                    return MessageEnum.Success;
                }
                else if (objCheck != null)
                {
                    return MessageEnum.Duplicate;
                }

                return MessageEnum.UnExpectedError;
            }
            catch
            {
                throw;
            }
        }

        public MessageEnum EmailAccountDelete(int emailAccountId, int DeletedBy)
        {
            try
            {
                var a = _Context.EmailAccounts.Where(x => x.EmailAccountId == emailAccountId).FirstOrDefault();
                if (a != null)
                {
                    a.Deleted = true;
                    a.UpdatedBy = DeletedBy;
                    a.UpdatedDate = DateTime.Now;
                    _Context.SaveChanges();
                    return MessageEnum.Deleted;
                }
                else
                    return MessageEnum.Invalid;
            }
            catch
            {
                throw;
            }
        }

        public MessageEnum EmailAccountUpdate(EmailAccountDTO emailAccountDTO)
        {
            try
            {
                var project = _Context.EmailAccounts.Where(x => x.EmailAccountId != emailAccountDTO.EmailAccountId && x.EmailAddress == emailAccountDTO.EmailAddress && x.Deleted == false).FirstOrDefault();
                if (project == null)
                {
                    var a = _Context.EmailAccounts.Where(x => x.EmailAccountId == emailAccountDTO.EmailAccountId).FirstOrDefault();
                    if (a != null)
                    {
                        a.EmailAddress = emailAccountDTO.EmailAddress;
                        a.Domain = emailAccountDTO.Domain;
                        a.EmailAccountName = emailAccountDTO.EmailAccountName;
                        a.EmailPassword = emailAccountDTO.EmailPassword;
                        a.AlternativeEmailAddress = emailAccountDTO.AlternativeEmailAddress;
                        a.EmailSignature = emailAccountDTO.EmailSignature;
                        a.EmailLogo = emailAccountDTO.EmailLogo;
                        a.IsDefaultSending = emailAccountDTO.IsDefaultSending;

                        a.UpdatedDate = DateTime.Now;
                        _Context.SaveChanges();
                        return MessageEnum.Updated;
                    }
                    else
                        return MessageEnum.Invalid;
                }
                else
                {
                    return MessageEnum.Duplicate;
                }

            }
            catch
            {
                throw;
            }
        }

        public EmailAccount GetEmailAccountByEmailAccountId(int emailAccountId)
        {
            try
            {
                return _Context.EmailAccounts.Where(x => x.EmailAccountId == emailAccountId && x.Deleted == false).SingleOrDefault();

            }
            catch
            {
                throw;
            }
        }

        public EmailAccount GetEmailAccountByEmailAccountName(string emailAccountName)
        {
            try
            {
                return _Context.EmailAccounts.Where(x => x.EmailAccountName == emailAccountName && x.Deleted == false).SingleOrDefault();

            }
            catch
            {
                throw;
            }
        }



        public List<EmailAccount> GetEmailAccounts()
        {
            try
            {
                return _Context.EmailAccounts.Where(x => x.Deleted == false).ToList();

            }
            catch
            {
                throw;
            }
        }

        public EmailAccount GetDefaultEmail(int? AccountId)
        {
            if (AccountId == null)
            {
                return _Context.EmailAccounts
                            .Include(c => c.EmailDomain).Where(x => x.Deleted == false && x.IsDefaultSending == true).FirstOrDefault();
            }
            else
            {
                return _Context.EmailAccounts
                    .Include(c => c.EmailDomain).Where(x => x.Deleted == false && x.EmailAccountId == AccountId).FirstOrDefault();
            }
        }
    }


}
