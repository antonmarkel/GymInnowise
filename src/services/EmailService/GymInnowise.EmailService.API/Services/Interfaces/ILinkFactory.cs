namespace GymInnowise.EmailService.API.Services.Interfaces
{
    public interface ILinkFactory
    {
        Task<string> GenerateVerificationLink(Guid token);
    }
}
