using FluentValidation;
using GymInnowise.Shared.Authorization;
using GymInnowise.Shared.Authorization.Dtos.RequestModels;

namespace GymInnowise.Authorization.API.Validators
{
    public class UpdateRolesRequestValidator : AbstractValidator<UpdateRolesRequest>
    {
        public UpdateRolesRequestValidator()
        {
            RuleForEach(rq => rq.Roles).Must(r => Roles.AllRoles.Contains(r)).WithMessage("Invalid role!");
        }
    }
}