using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CloudVOffice.Web.Model.User
{
    public partial record LoginModel
    {
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email Id")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }
    }

    public partial record ForgotPasswordModel
    {
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email Id")]
        public string Email { get; set; }

    }

    public partial record LoginModelForApi
    {
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email Id")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        public string ClientName { get; set; }
        public string ClientId { get; set; }
    }

    public class TokenModel
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string ClientName { get; set; }
        public string ClientId { get; set; }
    }
}
