using FluentValidation;
using GymInnowise.Authorization.Shared.Dtos.RequestModels;

namespace GymInnowise.Authorization.API.Validators
{
    public class RefreshTokenValidator : AbstractValidator<RefreshRequest>
    {
        private const int RefreshTokenLength = 88;

        public RefreshTokenValidator()
        {
            RuleFor(v => v.RefreshToken).Length(RefreshTokenLength).WithMessage("Invalid refresh token!");
        }
    }
}
