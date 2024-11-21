using FluentValidation;
using GymInnowise.SectionService.Configuration;
using GymInnowise.Shared.Sections.Dtos.Request;
using Microsoft.Extensions.Options;

namespace GymInnowise.SectionService.API.Validators
{
    public class CreateSectionRequestValidator : AbstractValidator<CreateSectionRequest>
    {
        private readonly SectionDataRestrictions _restrictions;

        public CreateSectionRequestValidator(IOptions<SectionDataRestrictions> restrictions)
        {
            _restrictions = restrictions.Value;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(_restrictions.SectionNameMaxLength)
                .WithMessage($"Name cannot exceed {_restrictions.SectionNameMaxLength} characters.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(_restrictions.SectionAboutMaxLength)
                .WithMessage($"Description cannot exceed {_restrictions.SectionAboutMaxLength} characters.");

            RuleFor(x => x.CostPerTraining)
                .GreaterThanOrEqualTo(0).WithMessage("Cost per training must be non-negative.");

            RuleFor(x => x.Tags)
                .Must(tags => tags.Length <= _restrictions.MaxTagAmount)
                .WithMessage($"A maximum of {_restrictions.MaxTagAmount} tags is allowed.")
                .ForEach(tag =>
                    tag
                        .NotEmpty()
                        .WithMessage("Tags cannot be empty")
                        .MaximumLength(_restrictions.TagMaxLength)
                        .WithMessage($"Tag cannot exceed {_restrictions.TagMaxLength} characters"));
        }
    }
}