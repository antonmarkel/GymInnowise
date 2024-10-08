using GymInnowise.EmailService.API.Controllers;
using GymInnowise.EmailService.API.Services.Interfaces;

namespace GymInnowise.EmailService.API.Services.Implementations
{
    public class LinkFactory(IHttpContextAccessor _contextAccessor, LinkGenerator _linkGenerator) : ILinkFactory
    {
        public Task<string> GenerateVerificationLink(Guid token)
        {
            var generatedLink = _linkGenerator.GetUriByName(_contextAccessor.HttpContext!,
                EmailController.VerificationEndpoint,
                values: new { token });

            return Task.FromResult<string>(generatedLink!);
        }
    }
}
