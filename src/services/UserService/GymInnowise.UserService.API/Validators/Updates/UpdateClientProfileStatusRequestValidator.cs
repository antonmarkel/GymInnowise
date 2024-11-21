using FluentValidation;
using GymInnowise.Shared.User.Dtos.RequestModels.Updates;
using GymInnowise.UserService.API.Extensions;

namespace GymInnowise.UserService.API.Validators.Updates
{
    public class UpdateClientProfileStatusRequestValidator : AbstractValidator<UpdateClientProfileStatusRequest>
    {
        public UpdateClientProfileStatusRequestValidator()
        {
            RuleFor(x => x.AccountStatus).AccountStatus();
            RuleFor(x => x.StatusNotes).Must(st => st != null)!.StatusNotes();
            RuleFor(x => x.ExpectedReturnDate)
                .Must(date => !date.HasValue || date.Value.Kind == DateTimeKind.Utc)
                .WithMessage("ExpectedReturnDate must be in UTC format.");
        }
    }
}