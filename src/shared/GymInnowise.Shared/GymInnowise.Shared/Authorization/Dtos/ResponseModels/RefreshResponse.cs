namespace GymInnowise.Shared.Authorization.Dtos.ResponseModels
{
    public class RefreshResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
