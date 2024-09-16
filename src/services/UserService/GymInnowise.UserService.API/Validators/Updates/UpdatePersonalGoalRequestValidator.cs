using FluentValidation;
using GymInnowise.UserService.API.Extensions;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;

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
