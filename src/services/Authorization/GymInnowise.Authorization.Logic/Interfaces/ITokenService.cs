using GymInnowise.Authorization.Persistence.Models.Enities;

namespace GymInnowise.Authorization.Logic.Interfaces
{
    public interface ITokenService
    {
        public string GenerateJwtToken(AccountEntity account);
        RefreshTokenEntity GenerateRefreshToken(Guid accountId);
        bool ValidateRefreshToken(RefreshTokenEntity refreshToken);
    }
}