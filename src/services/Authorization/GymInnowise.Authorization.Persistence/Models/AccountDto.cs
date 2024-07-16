
namespace GymInnowise.Authorization.Persistence.Models
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string[] Roles { get; set; }
    }
}
