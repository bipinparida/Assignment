namespace CloudVOffice.Core.Domain.Pemission
{
    public class Application : IAuditEntity, ISoftDeletedEntity
    {
        public int ApplicationId { get; set; }

        public string ApplicationName { get; set; }

        public int? Parent { get; set; }
        public bool IsGroup { get; set; }
        public string? Url { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int64? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        public string? IconImageUrl { get; set; }
        public string? IconClass { get; set; }
        public string AreaName { get; set; }
        public List<Application> Children { get; set; }

        public ICollection<RoleAndApplicationWisePermission> RoleAndModuleWisePermission { get; set; }
        public ICollection<UserWiseViewMapper> UserWiseViewMapper { get; set; }
    }
}
