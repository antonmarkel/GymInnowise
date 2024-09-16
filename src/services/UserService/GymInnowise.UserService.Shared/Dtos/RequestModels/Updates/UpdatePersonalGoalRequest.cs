﻿using GymInnowise.UserService.Shared.Enums;

namespace GymInnowise.UserService.Shared.Dtos.RequestModels.Updates
{
    public class UpdatePersonalGoalRequest
    {
        public string Goal { get; set; } = string.Empty;
        public Guid? SupervisorCoach { get; set; }
        public GoalStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? DeadLine { get; set; }
    }
}