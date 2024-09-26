using FluentValidation;
using GymInnowise.GymService.API.Validators.Base;
using GymInnowise.Shared.Gym.Dtos.Requests.Creates;

namespace GymInnowise.GymService.API.Validators.Creates
{
    public class CreateGymEventRequestValidator : AbstractValidator<CreateGymEventDtoRequest>
    {
        public CreateGymEventRequestValidator()
        {
            Include(new GymEventBaseDtoValidator());
        }
    }
}