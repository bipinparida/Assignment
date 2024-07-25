namespace CloudVOffice.Core.Domain.Users
{
    public class RefreshToken : IAuditEntity, ISoftDeletedEntity
    {
        public Int64 RefreshTokenId { get; set; }
        public Int64 UserId { get; set; }
        public string Refresh_Token { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string ClientName { get; set; }
        public string ClientId { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int64? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }
    }
}
