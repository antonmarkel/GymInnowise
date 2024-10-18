using GymInnowise.EmailService.Configuration.Templates;
using GymInnowise.EmailService.Logic.Interfaces;
using GymInnowise.Shared.Email.Messages;
using Microsoft.Extensions.Options;
using OneOf;
using OneOf.Types;
using Razor.Templating.Core;
using System.Text.Json;

namespace GymInnowise.EmailService.Logic.Services
{
    public class MessageBuilder : IMessageBuilder
    {
        private readonly TemplateSettings _templateSettings;

        public MessageBuilder(IOptions<TemplateSettings> templateSettings)
        {
            _templateSettings = templateSettings.Value;
        }

        public async Task<OneOf<Message, NotFound>> BuildMessageFromTemplateAsync(
            TemplateMessage templateMessage)
        {
            var viewName = $"/{_templateSettings.BasePath}/{templateMessage.Template}View.cshtml";

            string? modelJson = templateMessage.Model is JsonElement jsonElement
                ? jsonElement.GetRawText()
                : null;

            var htmlResult =
                await RazorTemplateEngine.TryRenderPartialAsync(viewName,
                    viewModel: modelJson);

            if (!htmlResult.ViewExists)
            {
                return new NotFound();
            }

            var message = new Message()
            {
                Body = htmlResult.RenderedView,
                Receiver = templateMessage.Receiver,
                Subject = templateMessage.Subject
            };

            return message;
        }
    }
}