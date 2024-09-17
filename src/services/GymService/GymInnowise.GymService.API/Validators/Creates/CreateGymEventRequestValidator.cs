using FluentValidation;
using GymInnowise.GymService.API.Validators.Base;
using GymInnowise.GymService.Shared.Dtos.Requests.Creates;

namespace GymInnowise.GymService.API.Validators.Creates
{
    public class CreateGymEventRequestValidator : AbstractValidator<CreateGymEventRequest>
    {
        public CreateGymEventRequestValidator()
        {
            Include(new GymEventBaseDtoValidator());
        }
    }
}
