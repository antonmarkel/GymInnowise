using FluentValidation;
using GymInnowise.Authorization.API.Extensions;
using GymInnowise.Authorization.Shared.Dtos.RequestModels;

namespace GymInnowise.Authorization.API.Validators
{
    public class RevokeRequestValidator : AbstractValidator<RevokeRequest>
    {
        public RevokeRequestValidator()
        {
            RuleFor(v => v.RefreshToken).RefreshToken();
        }
    }
}
