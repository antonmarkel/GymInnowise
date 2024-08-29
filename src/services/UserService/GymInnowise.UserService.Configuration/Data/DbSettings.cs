namespace GymInnowise.UserService.Configuration.Data
{
    public class DbSettings
    {
        public string Server { get; set; } = string.Empty;
        public string Database { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Port { get; set; }
    }
}
