﻿namespace GymInnowise.Shared.User.Dtos.RequestModels.Creates
{
    public class CreatePersonalGoalRequest
    {
        public string Goal { get; set; } = string.Empty;
        public Guid? SupervisorCoach { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? DeadLine { get; set; }
    }
}