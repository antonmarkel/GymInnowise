using FluentValidation;
using GymInnowise.Shared.User.Dtos.RequestModels.Creates;
using GymInnowise.UserService.API.Extensions;

namespace GymInnowise.UserService.API.Validators.Creates
{
    public class CreatePersonalGoalRequestValidator : AbstractValidator<CreatePersonalGoalRequest>
    {
        public CreatePersonalGoalRequestValidator()
        {
            RuleFor(x => x.Goal).Goal();
            RuleFor(x => x.SupervisorCoach).NullableIdentifier();
            RuleFor(model => model)
                .ValidateStartAndDeadline(model => model.StartDate, model => model.DeadLine);
        }
    }
}