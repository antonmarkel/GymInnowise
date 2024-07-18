using GymInnowise.Authorization.Shared.Dtos.Previews;

namespace GymInnowise.Authorization.Persistence.Models.Enities
{
    public class AccountEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<RoleEntity> Roles { get; set; } = new List<RoleEntity>();

        public AccountPreview ToPreview()
        {
            return new AccountPreview
            {
                Email = Email,
                PhoneNumber = PhoneNumber,
                Roles = Roles?.Select(r => r.RoleName)
            };
        }
    }
}
