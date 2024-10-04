namespace GymInnowise.EmailService.Configuration.Email
{
    public class EmailSettings
    {
        public required string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public required string SmtpUser { get; set; }
        public required string SmtpPass { get; set; }
        public required string FromAddress { get; set; }
        public bool EnableSsl { get; set; }
    }
}
