using CloudVOffice.Data.DTO.Users;

namespace CloudVOffice.Services.Users
{
    public interface ITokenService
    {
        public string CreateNewRefreshToken(TokenDTO tokenDTO);
        public string GetRefreshToken(RefreshTokenDTO tokenDTO);
    }
}
