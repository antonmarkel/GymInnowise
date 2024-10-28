using FluentValidation;
using GymInnowise.Shared.User.Dtos.RequestModels.Creates;
using GymInnowise.UserService.API.Extensions;

namespace GymInnowise.UserService.API.Validators.Creates
{
    public class CreateCoachProfileRequestValidator : AbstractValidator<CreateCoachProfileRequest>
    {
        public CreateCoachProfileRequestValidator()
        {
            Include(new CreateClientProfileRequestValidator());
            RuleFor(x => x.CostPerHour).Monetary();
        }
    }
}