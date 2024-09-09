﻿using GymInnowise.UserService.Shared.Enums;

namespace GymInnowise.UserService.Shared.Dtos.RequestModels.Updates
{
    public class UpdateClientProfileStatusRequest
    {
        public ClientStatus AccountStatus { get; set; }
        public string? StatusNotes { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }
    }
}
