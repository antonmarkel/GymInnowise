using FluentValidation;
using GymInnowise.Shared.User.Dtos.RequestModels.Updates;
using GymInnowise.UserService.API.Extensions;

namespace GymInnowise.UserService.API.Validators.Updates
{
    public class UpdateCoachProfileRequestValidator : AbstractValidator<UpdateCoachProfileRequest>
    {
        public UpdateCoachProfileRequestValidator()
        {
            Include(new UpdateClientProfileRequestValidator());
            RuleFor(x => x.CostPerHour).Monetary();
        }
    }
}