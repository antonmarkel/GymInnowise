﻿using GymInnowise.UserService.Shared.Enums;

namespace GymInnowise.UserService.Persistence.Models
{
    public class CoachProfileModel
    {
        public Guid AccountId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ClientStatus AccountStatus { get; set; }
        public string? StatusNotes { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }
        public List<TagEnum> Tags { get; set; } = [TagEnum.ToAdd];
        public DateTime HiredAt { get; set; }
        public decimal CostPerHour { get; set; }
        public CoachStatus CoachStatus { get; set; }
    }
}
