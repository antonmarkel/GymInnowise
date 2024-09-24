using FluentValidation;
using GymInnowise.UserService.API.Extensions;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;

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