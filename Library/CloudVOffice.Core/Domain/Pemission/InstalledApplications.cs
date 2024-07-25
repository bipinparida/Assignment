namespace CloudVOffice.Core.Domain.Pemission
{
    public class InstalledApplication : IAuditEntity, ISoftDeletedEntity
    {
        public int InstalledApplicationId { get; set; }
        public string PackageName { get; set; }
        public double Version { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int64? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }
    }
}
