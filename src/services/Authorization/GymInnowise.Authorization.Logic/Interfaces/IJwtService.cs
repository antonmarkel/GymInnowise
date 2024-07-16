namespace GymInnowise.Authorization.Logic.Interfaces
{
    public interface IJwtService
    {
        public string GenerateJwtToken(string phoneNumber, string email);
        //TODO: Add refreshToken methods

    }
}
