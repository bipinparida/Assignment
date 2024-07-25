namespace CloudVOffice.Core
{
    public interface IAuditEntity
    {
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int64? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
