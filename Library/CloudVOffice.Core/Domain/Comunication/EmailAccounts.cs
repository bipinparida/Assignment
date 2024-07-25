using System.ComponentModel.DataAnnotations.Schema;

namespace CloudVOffice.Core.Domain.Comunication
{
    public class EmailAccount : IAuditEntity, ISoftDeletedEntity
    {
        public int EmailAccountId { get; set; }

        public string EmailAddress { get; set; }
        public int Domain { get; set; }
        public string EmailAccountName { get; set; }
        public string EmailPassword { get; set; }
        public string AlternativeEmailAddress { get; set; }

        public string EmailSignature { get; set; }
        public string EmailLogo { get; set; }
        public bool IsDefaultSending { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int64? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }
        [ForeignKey("Domain")]
        public EmailDomain EmailDomain { get; set; }
    }
}
