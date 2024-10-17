namespace GymInnowise.EmailService.Logic.Interfaces
{
    public interface IEmailService
    {
        Task SendMessageAsync(string receiver, string subject, string body);
    }
}