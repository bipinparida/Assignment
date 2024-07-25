using CloudVOffice.Core.Domain.Common;
using CloudVOffice.Core.Domain.Comunication;
using CloudVOffice.Data.DTO.Comunication;
using CloudVOffice.Data.Persistence;
using CloudVOffice.Data.Repository;

namespace CloudVOffice.Services.Comunication
{
    public class EmailDomainService : IEmailDomainService
    {
        private readonly ApplicationDBContext _Context;
        private readonly ISqlRepository<EmailDomain> _emailDomainRepo;
        public EmailDomainService(ApplicationDBContext Context, ISqlRepository<EmailDomain> emailDomainRepo)
        {

            _Context = Context;
            _emailDomainRepo = emailDomainRepo;
        }
        public MessageEnum EmailDomainCreate(EmailDomainDTO emailDomainDTO)
        {
            var objCheck = _Context.EmailDomains.SingleOrDefault(opt => opt.DomainName == emailDomainDTO.DomainName && opt.Deleted == false);
            try
            {
                if (objCheck == null)
                {

                    EmailDomain emailDomain = new EmailDomain();
                    emailDomain.DomainName = emailDomainDTO.DomainName;
                    emailDomain.IncomingServer = emailDomainDTO.IncomingServer;
                    emailDomain.IncomingPort = emailDomainDTO.IncomingPort;
                    emailDomain.IncomingIsIMAP = emailDomainDTO.IncomingIsIMAP;
                    emailDomain.IncomingIsSsl = emailDomainDTO.IncomingIsSsl;
                    emailDomain.IncomingIsStartTLs = emailDomainDTO.IncomingIsStartTLs;
                    emailDomain.OutingServer = emailDomainDTO.OutingServer;
                    emailDomain.OutgoingPort = emailDomainDTO.OutgoingPort;
                    emailDomain.OutgoingIsTLs = emailDomainDTO.OutgoingIsTLs;
                    emailDomain.OutgoingIsSsl = emailDomainDTO.OutgoingIsSsl;
                    emailDomain.CreatedBy = emailDomainDTO.CreatedBy;
                    var obj = _emailDomainRepo.Insert(emailDomain);

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

        public MessageEnum EmailDomainDelete(int emailDomainId, int DeletedBy)
        {
            try
            {
                var a = _Context.EmailDomains.Where(x => x.EmailDomainId == emailDomainId).FirstOrDefault();
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

        public MessageEnum EmailDomainUpdate(EmailDomainDTO emailDomainDTO)
        {
            try
            {
                var project = _Context.EmailDomains.Where(x => x.EmailDomainId != emailDomainDTO.EmailDomainId && x.DomainName == emailDomainDTO.DomainName && x.Deleted == false).FirstOrDefault();
                if (project == null)
                {
                    var a = _Context.EmailDomains.Where(x => x.EmailDomainId == emailDomainDTO.EmailDomainId).FirstOrDefault();
                    if (a != null)
                    {
                        a.DomainName = emailDomainDTO.DomainName;
                        a.IncomingServer = emailDomainDTO.IncomingServer;
                        a.IncomingPort = emailDomainDTO.IncomingPort;
                        a.IncomingIsIMAP = emailDomainDTO.IncomingIsIMAP;
                        a.IncomingIsSsl = emailDomainDTO.IncomingIsSsl;
                        a.IncomingIsStartTLs = emailDomainDTO.IncomingIsStartTLs;
                        a.OutingServer = emailDomainDTO.OutingServer;
                        a.OutgoingPort = emailDomainDTO.OutgoingPort;
                        a.OutgoingIsTLs = emailDomainDTO.OutgoingIsTLs;
                        a.OutgoingIsSsl = emailDomainDTO.OutgoingIsSsl;
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

        public EmailDomain GetEmailDomainByEmailDomainId(int emailDomainId)
        {
            try
            {
                return _Context.EmailDomains.Where(x => x.EmailDomainId == emailDomainId && x.Deleted == false).SingleOrDefault();

            }
            catch
            {
                throw;
            }
        }

        public List<EmailDomain> GetEmailDomains()
        {
            try
            {
                return _Context.EmailDomains.Where(x => x.Deleted == false).ToList();

            }
            catch
            {
                throw;
            }
        }
    }


}
