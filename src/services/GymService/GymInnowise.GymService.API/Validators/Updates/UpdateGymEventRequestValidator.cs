using FluentValidation;
using GymInnowise.GymService.API.Validators.Base;
using GymInnowise.Shared.Gym.Dtos.Requests.Updates;

namespace GymInnowise.GymService.API.Validators.Updates
{
    public class UpdateGymEventRequestValidator : AbstractValidator<UpdateGymEventDtoRequest>
    {
        public UpdateGymEventRequestValidator()
        {
            Include(new GymEventBaseDtoValidator());
        }
    }
}