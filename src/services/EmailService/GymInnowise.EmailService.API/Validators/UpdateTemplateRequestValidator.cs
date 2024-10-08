using FluentValidation;
using GymInnowise.EmailService.Logic.Services;
using GymInnowise.EmailService.Shared.Dtos.Requests;

namespace GymInnowise.EmailService.API.Validators
{
    public class UpdateTemplateRequestValidator : AbstractValidator<UpdateTemplateRequest>
    {
        public UpdateTemplateRequestValidator()
        {
            RuleFor(req => req).Must(req => MessageMapper.CanBeMapped(req.Body, req.Keys));
        }
    }
}
