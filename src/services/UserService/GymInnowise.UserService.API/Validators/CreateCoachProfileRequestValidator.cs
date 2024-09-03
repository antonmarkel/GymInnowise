﻿using FluentValidation;
using GymInnowise.UserService.API.Extensions;
using GymInnowise.UserService.Shared.Dtos.RequestModels.Creates;

namespace GymInnowise.UserService.API.Validators
{
    public class CreateCoachProfileRequestValidator : AbstractValidator<CreateCoachProfileRequest>
    {
        public CreateCoachProfileRequestValidator()
        {
            RuleFor(x => x.AccountId).Identifier();
            RuleFor(x => x.FirstName).FirstName();
            RuleFor(x => x.LastName).LastName();
            RuleFor(x => x.DateOfBirth).DateOfBirth();
            RuleFor(x => x.Gender).Must(gender => gender != null)!.Gender();
        }
    }
}
