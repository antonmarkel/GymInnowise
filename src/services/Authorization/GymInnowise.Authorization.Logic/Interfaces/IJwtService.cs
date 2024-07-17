using GymInnowise.Authorization.Shared.Dtos.Previews;

namespace GymInnowise.Authorization.Logic.Interfaces
{
    public interface IJwtService
    {
        public string GenerateJwtToken(AccountPreview account);
        //TODO: Add refreshToken methods
    }
}
