using FluentValidation;
using GymInnowise.UserService.API.Extensions;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;

namespace GymInnowise.UserService.API.Validators
{
    public class CreatePersonalGoalRequestValidator : AbstractValidator<CreatePersonalGoalRequest>
    {
        public CreatePersonalGoalRequestValidator()
        {
            RuleFor(x => x.Owner).Identifier();
            RuleFor(x => x.Goal).Goal();
            RuleFor(x => x.SupervisorCoach)
                .Must(sc => sc == null || sc != Guid.Empty)
                .WithMessage("SupervisorCoach, if provided, must be a valid GUID.");

            RuleFor(x => x.StartDate)
                .Must(date => date.Kind == DateTimeKind.Utc)
                .WithMessage("StartDate must be in UTC format.")
                .GreaterThanOrEqualTo(DateTime.Today.ToUniversalTime())
                .WithMessage("StartDate cannot be in the past.");

            RuleFor(x => x.DeadLine)
                .Must(date => date != null && date.Value.Kind == DateTimeKind.Utc)
                .WithMessage("DeadLine must be in UTC format.")
                .Must((model, deadline) => !deadline.HasValue || deadline.Value > model.StartDate)
                .WithMessage("DeadLine, if provided, must be after the StartDate.");
        }
    }
}
