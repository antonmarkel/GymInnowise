using FluentValidation;
using GymInnowise.SectionService.Configuration;
using GymInnowise.Shared.Sections.Base.Relations;
using Microsoft.Extensions.Options;

namespace GymInnowise.SectionService.API.Validators
{
    public class MentorshipBaseValidator : AbstractValidator<MentorshipBase>
    {
        private readonly SectionDataRestrictions _restrictions;

        public MentorshipBaseValidator(IOptions<SectionDataRestrictions> restrictions)
        {
            _restrictions = restrictions.Value;
            RuleFor(rel => rel.Notes).MaximumLength(_restrictions.SectionCoachNotesMaxLength)
                .WithMessage($"Gym notes cannot exceed {_restrictions.SectionCoachNotesMaxLength} characters");
        }
    }
}
