using GymInnowise.Authorization.Configuration.Token;
using GymInnowise.Authorization.Logic.Interfaces;
using GymInnowise.Authorization.Persistence.Models.Enities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GymInnowise.Authorization.Logic.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateJwtToken(AccountEntity account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

            var claims = new List<Claim>();
            foreach (var role in account.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            claims.Add(new Claim("accountId", account.Id.ToString()));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public RefreshTokenEntity GenerateRefreshToken(Guid accountId)
        {
            var refreshToken = new RefreshTokenEntity
            {
                AccountId = accountId,
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                CreatedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMinutes(_jwtSettings.RefreshExpiryInMinutes),
            };

            return refreshToken;
        }

        public bool ValidateRefreshToken(RefreshTokenEntity refreshToken)
        {
            return refreshToken.ExpiryDate > DateTime.UtcNow;
        }
    }
}
