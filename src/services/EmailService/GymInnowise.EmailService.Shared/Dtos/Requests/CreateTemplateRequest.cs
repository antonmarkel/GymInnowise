using GymInnowise.EmailService.Shared.Dtos.Base;

namespace GymInnowise.EmailService.Shared.Dtos.Requests
{
    public class CreateTemplateRequest : TemplateBase
    {
        public required string TemplateName { get; set; }
    }
}
