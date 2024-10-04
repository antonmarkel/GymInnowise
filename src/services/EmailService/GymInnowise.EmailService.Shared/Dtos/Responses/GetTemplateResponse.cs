using GymInnowise.EmailService.Shared.Dtos.Base;

namespace GymInnowise.EmailService.Shared.Dtos.Responses
{
    public class GetTemplateResponse : TemplateBase
    {
        public required string TemplateName { get; set; }
    }
}
