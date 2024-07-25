using System.Text.Json.Serialization;

namespace CloudVOffice.Core.Domain.Users
{
    public partial class UserRoleMapping
    {
        public int UserRoleMappingId { get; set; }
        public Int64 UserId { get; set; }

        /// <summary>
        /// Gets or sets the customer role identifier
        /// </summary>
        public int RoleId { get; set; }



        [JsonIgnore]
        public Role Role { get; set; }
        [JsonIgnore]
        public User User { get; set; }

    }
}
