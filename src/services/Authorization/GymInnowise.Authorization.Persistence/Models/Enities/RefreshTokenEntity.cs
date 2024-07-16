using System.ComponentModel.DataAnnotations;

namespace GymInnowise.Authorization.Persistence.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        [Required]
        public string Token { get; set; }
        public Account Account { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime RevokedDate { get; set; }
        public bool isRevoked { get; set; }
    }
}
