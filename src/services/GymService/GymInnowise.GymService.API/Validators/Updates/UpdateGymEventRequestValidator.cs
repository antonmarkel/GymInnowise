﻿using FluentValidation;
using GymInnowise.GymService.API.Validators.Base;
using GymInnowise.GymService.Shared.Dtos.Requests.Updates;

namespace GymInnowise.GymService.API.Validators.Updates
{
    public class UpdateGymEventRequestValidator : AbstractValidator<UpdateGymEventDtoRequest>
    {
        public UpdateGymEventRequestValidator()
        {
            Include(new GymEventBaseDtoValidator());
        }
    }
}