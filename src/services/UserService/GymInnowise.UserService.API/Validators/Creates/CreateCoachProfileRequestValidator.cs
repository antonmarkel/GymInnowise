using FluentValidation;
using GymInnowise.UserService.API.Extensions;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;

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
