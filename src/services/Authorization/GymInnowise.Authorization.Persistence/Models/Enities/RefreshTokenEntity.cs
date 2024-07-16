using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymInnowise.Authorization.Persistence.Models.Enities
{
    public class RefreshTokenEntity
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = null!;
        public AccountEntity Account { get; set; } = null!;
        public DateTime? ExpiryDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? RevokedDate { get; set; }
        public bool isRevoked { get; set; }
    }
}
