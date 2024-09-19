using FluentValidation;
using GymInnowise.UserService.API.Extensions;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Updates;

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
