using FluentValidation;
using GymInnowise.EmailService.Logic.Services;
using GymInnowise.EmailService.Shared.Dtos.Requests;

namespace GymInnowise.EmailService.API.Validators
{
    public class CreateTemplateRequestValidator : AbstractValidator<CreateTemplateRequest>
    {
        public CreateTemplateRequestValidator()
        {
            RuleFor(req => req).Must(req => MessageMapper.CanBeMapped(req.Body, req.Keys));
        }
    }
}
