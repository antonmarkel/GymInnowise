using FluentValidation;
using GymInnowise.Shared.User.Dtos.RequestModels.Updates;

namespace GymInnowise.UserService.API.Validators.Updates
{
    public class UpdateCoachProfileStatusRequestValidator : AbstractValidator<UpdateCoachProfileStatusRequest>
    {
        public UpdateCoachProfileStatusRequestValidator()
        {
            Include(new UpdateClientProfileStatusRequestValidator());
            RuleFor(x => x.CoachStatus)
                .IsInEnum()
                .WithMessage("CoachStatus is invalid.");
        }
    }
}