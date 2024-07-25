namespace CloudVOffice.Web.Model.User
{
    public class SetPasswordModel
    {
        public string Password { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
    }

	public class ChangePasswordModel
	{
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }

	}
}
