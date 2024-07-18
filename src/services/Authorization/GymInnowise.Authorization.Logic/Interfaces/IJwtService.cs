using GymInnowise.Authorization.Persistence.Models.Enities;

namespace GymInnowise.Authorization.Logic.Interfaces
{
    public interface IJwtService
    {
        public string GenerateJwtToken(AccountEntity account);
        //TODO: Add refreshToken methods
    }
}
