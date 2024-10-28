using FluentValidation;
using GymInnowise.GymService.API.Validators.Base;
using GymInnowise.Shared.Gym.Dtos.Requests.Updates;

namespace GymInnowise.GymService.API.Validators.Updates
{
    public class UpdateGymRequestValidator : AbstractValidator<UpdateGymRequest>
    {
        public UpdateGymRequestValidator()
        {
            Include(new GymDetailsBaseDtoValidator());
        }
    }
}