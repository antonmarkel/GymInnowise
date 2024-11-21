using GymInnowise.SectionService.API.Authorization.Requirements;
using GymInnowise.SectionService.Logic.Queries;
using GymInnowise.Shared.Authorization;
using GymInnowise.Shared.Sections.Base.Relations;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace GymInnowise.SectionService.API.Authorization.Handlers
{
    public class MentorOrSuperiorHandler : AuthorizationHandler<MentorOrSuperiorRequirement>
    {
        private readonly ISender _sender;

        public MentorOrSuperiorHandler(ISender sender)
        {
            _sender = sender;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            MentorOrSuperiorRequirement requirement)
        {
            if (context.User.IsInRole(Roles.Admin))
            {
                context.Succeed(requirement);

                return;
            }

            if (!context.User.IsInRole(Roles.Coach))
            {
                return;
            }

            var relation =
                await _sender.Send(
                    new GetSectionRelationQuery<MentorshipBase>(requirement.SectionId, requirement.AccountId));

            if (relation.IsT0)
            {
                context.Succeed(requirement);
            }
        }
    }
}
