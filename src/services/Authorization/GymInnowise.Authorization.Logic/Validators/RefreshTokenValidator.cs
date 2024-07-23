using FluentValidation;
using GymInnowise.Authorization.Shared.Dtos.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymInnowise.Authorization.Logic.Validators
{
    public class RefreshTokenValidator : AbstractValidator<string>
    {
        public RefreshTokenValidator()
        {
            RuleFor(v => v).Length(88);
        }
    }
}
