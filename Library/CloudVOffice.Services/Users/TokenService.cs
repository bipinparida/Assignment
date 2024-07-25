using CloudVOffice.Core.Domain.Users;
using CloudVOffice.Data.DTO.Users;
using CloudVOffice.Data.Persistence;
using CloudVOffice.Data.Repository;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace CloudVOffice.Services.Users
{
    public class TokenService : ITokenService
    {
        private readonly ApplicationDBContext _context;
        private readonly ISqlRepository<RefreshToken> _refreshTokenRepo;
        private readonly IConfiguration _configuration;
        public TokenService(ApplicationDBContext context, ISqlRepository<RefreshToken> refreshTokenRepo, IConfiguration configuration)
        {
            _context = context;
            _refreshTokenRepo = refreshTokenRepo;
            _configuration = configuration;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public string CreateNewRefreshToken(TokenDTO tokenDTO)
        {
            var a = tokenDTO.ClientName.Contains("Windows") || tokenDTO.ClientName.Contains("PC") || tokenDTO.ClientName.Contains("Desktop") ? _context.RefreshTokens.Where(x => x.Deleted == false && x.UserId == tokenDTO.UserId && x.ClientName == tokenDTO.ClientName && x.ClientId == tokenDTO.ClientId).FirstOrDefault() : _context.RefreshTokens.Where(x => x.Deleted == false && x.UserId == tokenDTO.UserId && x.ClientName == tokenDTO.ClientName && x.ClientId == tokenDTO.ClientId && x.RefreshTokenExpiryTime > DateTime.Now).FirstOrDefault();
            if (a != null)
            {
                return a.Refresh_Token;
            }
            else
            {
                RefreshToken refreshToken = new RefreshToken();
                if (tokenDTO.ClientName.Contains("Windows") || tokenDTO.ClientName.Contains("PC") || tokenDTO.ClientName.Contains("Desktop"))
                {
                    refreshToken.RefreshTokenExpiryTime = null;
                }
                else
                {
                    _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int tokenValidityInMinutes);
                    refreshToken.RefreshTokenExpiryTime = DateTime.Now.AddDays(tokenValidityInMinutes);
                }
                refreshToken.UserId = tokenDTO.UserId;
                refreshToken.ClientId = tokenDTO.ClientId;
                refreshToken.ClientName = tokenDTO.ClientName;
                refreshToken.Refresh_Token = GenerateRefreshToken();
                _refreshTokenRepo.Insert(refreshToken);
                return refreshToken.Refresh_Token;

            }
        }

        public string GetRefreshToken(RefreshTokenDTO tokenDTO)
        {

            var a = tokenDTO.ClientName.Contains("Windows") || tokenDTO.ClientName.Contains("PC") || tokenDTO.ClientName.Contains("Desktop") ? _context.RefreshTokens.Where(x => x.Deleted == false && x.UserId == tokenDTO.UserId && x.ClientName == tokenDTO.ClientName && x.ClientId == tokenDTO.ClientId && x.Refresh_Token == tokenDTO.Refresh_Token).FirstOrDefault() : _context.RefreshTokens.Where(x => x.Deleted == false && x.UserId == tokenDTO.UserId && x.ClientName == tokenDTO.ClientName && x.ClientId == tokenDTO.ClientId && x.RefreshTokenExpiryTime > DateTime.Now && x.Refresh_Token == tokenDTO.Refresh_Token).FirstOrDefault();
            return a != null ? a.Refresh_Token : "";


        }
    }
}
