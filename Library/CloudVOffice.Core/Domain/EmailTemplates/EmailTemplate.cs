namespace CloudVOffice.Core.Domain.EmailTemplates
{
    public class EmailTemplate : IAuditEntity, ISoftDeletedEntity
    {
        public int EmailTemplateId { get; set; }
        public string EmailTemplateName { get; set; }
        public string EmailTemplateDescription { get; set; }
        public string Subject { get; set; }

        public int? DefaultSendingAccount { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int64? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }

    }
}
