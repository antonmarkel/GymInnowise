using System.ComponentModel.DataAnnotations;

namespace GymInnowise.Authorization.Persistence.Models.Enities
{
    public class RefreshTokenEntity
    {
        public Guid Id { get; set; }
        [Required]
        public string Token { get; set; } = null!;
        [Required]
        public AccountEnity Account { get; set; } = null!;
        public DateTime ExpiryDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime RevokedDate { get; set; }
        public bool isRevoked { get; set; }
    }
}
