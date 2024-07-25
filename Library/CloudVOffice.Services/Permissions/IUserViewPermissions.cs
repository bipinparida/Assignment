namespace CloudVOffice.Services.Permissions
{
    public interface IUserViewPermissions
    {
        public string AssignViewPermissions(Int64 UserId, int RoleId);
        public string UnAssignViewPermissions(Int64 UserId, int RoleId);
    }
}
