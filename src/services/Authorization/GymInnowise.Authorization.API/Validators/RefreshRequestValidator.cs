using FluentValidation;
using GymInnowise.Authorization.API.Extensions;
using GymInnowise.Shared.Authorization.Dtos.RequestModels;

namespace GymInnowise.Authorization.API.Validators
{
    public class RefreshRequestValidator : AbstractValidator<RefreshRequest>
    {
        public RefreshRequestValidator()
        {
            RuleFor(rq => rq.RefreshToken).RefreshToken();
        }
    }
}