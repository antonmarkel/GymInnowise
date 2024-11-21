using FluentValidation;
using GymInnowise.SectionService.Configuration;
using GymInnowise.Shared.Sections.Base.Relations;
using Microsoft.Extensions.Options;

namespace GymInnowise.SectionService.API.Validators
{
    public class GymRelationBaseValidator : AbstractValidator<GymRelationBase>
    {
        private readonly SectionDataRestrictions _restrictions;

        public GymRelationBaseValidator(IOptions<SectionDataRestrictions> restrictions)
        {
            _restrictions = restrictions.Value;
            RuleFor(rel => rel.Notes).MaximumLength(_restrictions.SectionGymNotesMaxLength)
                .WithMessage($"Gym notes cannot exceed {_restrictions.SectionGymNotesMaxLength} characters");
        }
    }
}
