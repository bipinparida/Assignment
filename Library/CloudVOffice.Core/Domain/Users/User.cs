using CloudVOffice.Core.Domain.Pemission;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudVOffice.Core.Domain.Users
{
    public class User : IAuditEntity, ISoftDeletedEntity
    {
        public Int64 UserId { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                if (MiddleName != null && MiddleName != "")
                {
                    return FirstName + " " + MiddleName + " " + LastName;
                }
                else
                { return FirstName + " " + LastName; }

            }
        }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNo { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public int? FailedLoginAttempts { get; set; }

        public string? LastIpAddress { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime LastActivityDate { get; set; }
        public bool IsActive { get; set; }
        public int UserTypeId { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int64? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }

        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordTokenExpirey { get; set; }
        public UserType UserType
        {
            get => (UserType)UserTypeId;
            set => UserTypeId = (int)value;
        }

        public List<UserRoleMapping> UserRoleMappings { get; set; }
        public List<UserWiseViewMapper> UserWiseViewMapper { get; set; }





    }
}
