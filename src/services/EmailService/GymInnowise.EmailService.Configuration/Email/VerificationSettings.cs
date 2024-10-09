namespace GymInnowise.EmailService.Configuration.Email
{
    public class VerificationSettings
    {
        public int ExpireAfterMinutes { get; set; }
        public required string BaseUrl { get; set; }
    }
}
