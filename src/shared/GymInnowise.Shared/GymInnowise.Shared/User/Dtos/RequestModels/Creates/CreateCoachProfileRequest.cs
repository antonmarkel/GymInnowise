﻿namespace GymInnowise.Shared.User.Dtos.RequestModels.Creates
{
    public class CreateCoachProfileRequest : CreateClientProfileRequest
    {
        public decimal CostPerHour { get; set; }
    }
}