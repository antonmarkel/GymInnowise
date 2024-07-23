using FluentValidation;
using GymInnowise.Authorization.Shared.Dtos.RequestModels;

namespace GymInnowise.Authorization.API.Validators
{
    public class RevokeTokenValidator : AbstractValidator<RevokeRequest>
    {
        private const int RefreshTokenLength = 88;

        public RevokeTokenValidator()
        {
            RuleFor(v => v.RefreshToken).Length(RefreshTokenLength).WithMessage("Invalid refresh token!");
        }
    }
}
