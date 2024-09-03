using FluentValidation;
using GymInnowise.UserService.API.Extensions;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;

namespace GymInnowise.UserService.API.Validators
{
    public class UpdatePersonalGoalRequestValidator : AbstractValidator<UpdatePersonalGoalRequest>
    {
        public UpdatePersonalGoalRequestValidator()
        {
            RuleFor(x => x.Id).Identifier();
            RuleFor(x => x.Goal).Goal();

            RuleFor(x => x.SupervisorCoach)
                .Must(sc => sc == null || sc != Guid.Empty)
                .WithMessage("SupervisorCoach, if provided, must be a valid GUID.");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Status is invalid.");

            RuleFor(x => x.StartDate)
                .Must(date => date.Kind == DateTimeKind.Utc)
                .WithMessage("StartDate must be in UTC format.")
                .GreaterThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("StartDate cannot be in the past.");

            RuleFor(x => x.DeadLine)
                .Must((model, deadline) => !deadline.HasValue ||
                                           (deadline.Value.Kind == DateTimeKind.Utc &&
                                            deadline.Value > model.StartDate))
                .WithMessage("DeadLine, if provided, must be in UTC format and after the StartDate.");
        }
    }
}
