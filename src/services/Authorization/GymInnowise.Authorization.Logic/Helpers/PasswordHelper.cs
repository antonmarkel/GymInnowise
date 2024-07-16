namespace GymInnowise.Authorization.Logic.Helpers
{
    public class PasswordHelper
    {
        public static string HashPassword(string password) =>
         BCrypt.Net.BCrypt.HashPassword(password);

        public static bool VerifyPassword(string password, string passwordHash) =>
            BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
