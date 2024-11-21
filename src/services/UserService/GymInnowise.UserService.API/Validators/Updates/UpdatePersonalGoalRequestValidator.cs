using FluentValidation;
using GymInnowise.Shared.User.Dtos.RequestModels.Updates;
using GymInnowise.UserService.API.Extensions;

namespace GymInnowise.UserService.API.Validators.Updates
{
    public class UpdatePersonalGoalRequestValidator : AbstractValidator<UpdatePersonalGoalRequest>
    {
        public UpdatePersonalGoalRequestValidator()
        {
            RuleFor(x => x.Goal).Goal();
            RuleFor(x => x.SupervisorCoach).NullableIdentifier();
            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Status is invalid.");
            RuleFor(model => model)
                .ValidateStartAndDeadline(model => model.StartDate, model => model.DeadLine);
        }
    }
}