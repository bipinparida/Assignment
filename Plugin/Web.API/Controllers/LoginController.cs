using CloudVOffice.Core.Domain.Users;
using CloudVOffice.Data.DTO.Users;
using CloudVOffice.Services.Authentication;
using CloudVOffice.Services.Users;
using CloudVOffice.Web.Model.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Web.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IUserAuthenticationService _userauthenticationService;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        public LoginController(IUserAuthenticationService userauthenticationService,
            IUserService userService,
            ITokenService tokenService,
            IConfiguration configuration
            )
        {
            _userauthenticationService = userauthenticationService;
            _userService = userService;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }
        [HttpPost]
        public async Task<IActionResult> Auth(LoginModelForApi model)
        {
            if (ModelState.IsValid)
            {
                var Email = model.Email?.Trim();
                var loginResult = await _userauthenticationService.ValidateUserAsync(Email, model.Password);
                switch (loginResult)
                {
                    case UserLoginResults.Successful:
                        {
                            var userDetails = await _userService.GetUserByEmailAsync(Email);
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Email, userDetails.Email),
                                new Claim("FirstName",userDetails.FirstName),
                                new Claim("MiddleName",userDetails.MiddleName!=null?userDetails.MiddleName.ToString():""),
                                new Claim("LastName",userDetails.LastName!=null?userDetails.LastName.ToString():""),
                                new Claim("UserId",userDetails.UserId.ToString()),
								//  new Claim("Menu",menujson),
							};
                            var a = userDetails.UserRoleMappings;
                            claims.AddRange(userDetails.UserRoleMappings.Select(role => new Claim(ClaimTypes.Role, role.Role.RoleName)));
                            TokenDTO tokenDTO = new TokenDTO
                            {
                                ClientId = model.ClientId,
                                ClientName = model.ClientName,
                                UserId = userDetails.UserId
                            };
                            string refreshToken = _tokenService.CreateNewRefreshToken(tokenDTO);

                            var token = CreateToken(claims);
                            return Ok(new
                            {
                                Token = new JwtSecurityTokenHandler().WriteToken(token),
                                RefreshToken = refreshToken,
                                Expiration = token.ValidTo
                            });


                        }
                    case UserLoginResults.UserNotExist:
                        {

                            return BadRequest("User Not Exists.");

                        }
                    case UserLoginResults.Deleted:
                        BadRequest("Account Has Been Deleted.");
                        break;

                    case UserLoginResults.NotActive:
                        BadRequest("Account Has Been Suspended.");
                        break;

                    default:
                        BadRequest("Invalid Credentials");
                        break;
                }
            }
            return BadRequest(model);
        }
        [HttpPost]

        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }
            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;
            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string? userid = principal.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (userid == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }
            string? nrefreshToken = _tokenService.GetRefreshToken(new RefreshTokenDTO
            {
                UserId = Int64.Parse(userid),
                ClientId = tokenModel.ClientId,
                ClientName = tokenModel.ClientName,
                Refresh_Token = tokenModel.RefreshToken
            });
            if (nrefreshToken == "" || nrefreshToken == null || nrefreshToken != tokenModel.RefreshToken)
            {
                return BadRequest("Invalid access token or refresh token");
            }
            var newAccessToken = CreateToken(principal.Claims.ToList());
            return Ok(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = nrefreshToken
            });

        }
    }
}
