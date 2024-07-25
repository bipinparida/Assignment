namespace CloudVOffice.Data.DTO.Users
{
    public class TokenDTO
    {
        public Int64 UserId { get; set; }
        public string ClientName { get; set; }
        public string ClientId { get; set; }

    }

    public class RefreshTokenDTO
    {
        public Int64 UserId { get; set; }
        public string ClientName { get; set; }
        public string ClientId { get; set; }
        public string Refresh_Token { get; set; }

    }
}
