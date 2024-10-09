using GymInnowise.EmailService.API.Controllers;
using GymInnowise.EmailService.API.Services.Interfaces;
using GymInnowise.EmailService.Configuration.Email;
using Microsoft.Extensions.Options;

namespace GymInnowise.EmailService.API.Services.Implementations
{
    public class VerificationLinkFactory(LinkGenerator _linkGenerator, IOptions<VerificationSettings> _settings)
        : ILinkFactory
    {
        public Task<string> GenerateVerificationLink(Guid token)
        {
            var scheme = Environment.GetEnvironmentVariable("SERVICE_SCHEME") ?? "https";
            var host = Environment.GetEnvironmentVariable("SERVICE_HOST") ?? "my-domain.com";

            var verifyEmailUrl = _linkGenerator.GetPathByAction(
                action: EmailController.VerificationEndpoint,
                controller: "Email",
                values: new { token }
            );

            return Task.FromResult(_settings.Value.BaseUrl + verifyEmailUrl!);
        }
    }
}
