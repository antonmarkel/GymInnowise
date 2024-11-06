using FluentValidation;
using FluentValidation.AspNetCore;
using GymInnowise.SectionService.Configuration;
using GymInnowise.Shared.Sections.Base.Relations;
using Microsoft.Extensions.Options;

namespace GymInnowise.SectionService.API.Validators
{
    public class MembershipBaseValidator : AbstractValidator<MembershipBase>
    {
        private readonly SectionDataRestrictions _restrictions;

        public MembershipBaseValidator(IOptions<SectionDataRestrictions> restrictions)
        {
            _restrictions = restrictions.Value;
            RuleFor(rel => rel.Goal).MaximumLength(_restrictions.SectionMemberGoalMaxLength)
                .WithMessage($"Goal must not exceed {_restrictions.SectionMemberGoalMaxLength} characters");
        }
    }
}
