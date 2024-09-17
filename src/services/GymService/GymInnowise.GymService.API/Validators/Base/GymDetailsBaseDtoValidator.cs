using FluentValidation;
using GymInnowise.GymService.Shared.Dtos.Abstract;

namespace GymInnowise.GymService.API.Validators.Base
{
    public class GymDetailsBaseDtoValidator : AbstractValidator<GymDetailsBaseDto>
    {
        public GymDetailsBaseDtoValidator()
        {
            RuleFor(gym => gym.Name)
                .NotEmpty().WithMessage("Gym name is required.")
                .Length(3, 100).WithMessage("Gym name must be between 3 and 100 characters.");

            RuleFor(gym => gym.Address)
                .NotEmpty().WithMessage("Address is required.")
                .Length(5, 200).WithMessage("Address must be between 5 and 200 characters.");

            RuleFor(gym => gym.ContactInfo)
                .NotEmpty().WithMessage("Contact information is required.")
                .MinimumLength(10).WithMessage("Contact information must be at least 10 characters long.");

            RuleFor(gym => gym.MaxOccupancy)
                .GreaterThan(0).WithMessage("Max occupancy must be greater than 0.");

            RuleFor(gym => gym.SquareFootage)
                .GreaterThan(0).WithMessage("Square footage must be a positive number.");

            RuleFor(gym => gym)
                .Must(gym => gym.OpenTime < gym.CloseTime)
                .WithMessage("Opening time must be earlier than closing time.");

            RuleFor(gym => gym.DaysAvailableMask)
                .InclusiveBetween((byte)1, (byte)127).WithMessage("DaysAvailableMask must be between 1 and 127.");

            RuleFor(gym => gym.CostValue)
                .GreaterThanOrEqualTo(0).WithMessage("Cost value must be non-negative.");

            RuleFor(gym => gym.PayType)
                .IsInEnum().WithMessage("Invalid payment type.");

            RuleFor(gym => gym.Tags)
                .NotEmpty().WithMessage("At least one tag is required.");

            RuleForEach(gym => gym.Tags)
                .IsInEnum().WithMessage("Invalid tag.");
        }
    }
}
