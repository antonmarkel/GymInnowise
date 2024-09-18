using FluentValidation;
using GymInnowise.GymService.Shared.Dtos.Abstract;

namespace GymInnowise.GymService.API.Validators.Base
{
    public class GymEventBaseDtoValidator : AbstractValidator<GymEventBaseDto>
    {
        public GymEventBaseDtoValidator()
        {
            RuleFor(dto => dto.CreatedBy)
                .NotEmpty().WithMessage("CreatedBy is required.");

            RuleFor(dto => dto.TrainingId)
                .Must(id => id == null || id != Guid.Empty)
                .WithMessage("TrainingId, if provided, must be a valid GUID.");

            RuleFor(dto => dto.EventType)
                .IsInEnum().WithMessage("EventType must be valid.");

            RuleFor(dto => dto.Info)
                .MaximumLength(500).WithMessage("Info cannot exceed 500 characters.");

            RuleFor(dto => dto)
                .Must(dto => dto.StartTime < dto.EndTime)
                .WithMessage("StartTime must be earlier than EndTime.");

            RuleFor(dto => dto.CreatedAt)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("CreatedAt cannot be in the future.");
        }
    }
}
