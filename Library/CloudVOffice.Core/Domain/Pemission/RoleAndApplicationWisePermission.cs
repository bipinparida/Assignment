using CloudVOffice.Core.Domain.Users;
using Newtonsoft.Json;

namespace CloudVOffice.Core.Domain.Pemission
{
    public class RoleAndApplicationWisePermission : IAuditEntity, ISoftDeletedEntity
    {
        public Int64 RoleAndApplicationWisePermissionId { get; set; }
        public int ApplicationId { get; set; }
        public int RoleId { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int64? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        [JsonIgnore]
        public Application Application { get; set; }
        [JsonIgnore]
        public Role Role { get; set; }
    }
}
