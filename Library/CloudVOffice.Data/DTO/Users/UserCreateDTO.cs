using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CloudVOffice.Data.DTO.Users
{
    public class UserCreateDTO
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string? MiddleName { get; set; }
        [DisplayName("Last Name")]
        public string? LastName { get; set; }

        public string? Password { get; set; }
        [Required]
        [DisplayName("Email Id")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string? PhoneNo { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [DisplayName("User Type")]
        public int UserTypeId { get; set; }
        public Int64 CreatedBy { get; set; }
        public List<UserRolesDTO> roles { get; set; }
        public long? UserId { get; set; }
        public bool WantToStayOnThisPage { get; set; }
    }
    public class UserRolesDTO
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
