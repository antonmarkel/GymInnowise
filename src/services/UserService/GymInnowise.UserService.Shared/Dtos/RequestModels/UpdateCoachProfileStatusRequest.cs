using GymInnowise.UserService.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymInnowise.UserService.Shared.Dtos.RequestModels
{
    public class UpdateCoachProfileStatusRequest
    {
        public Guid AccountId { get; set; }
        public ClientStatus AccountStatus { get; set; }
        public string? StatusNotes { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }
        public CoachStatus CoachStatus { get; set; }
    }
}
