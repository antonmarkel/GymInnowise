﻿using GymInnowise.Shared.User.Dtos.RequestModels.Updates;

namespace GymInnowise.Shared.RabbitMq.Events.Profiles
{
    public class CoachProfileUpdatedEvent
    {
        public required Guid AccountId { get; set; }
        public required UpdateCoachProfileRequest UpdateProfileRequest { get; set; }
    }
}
