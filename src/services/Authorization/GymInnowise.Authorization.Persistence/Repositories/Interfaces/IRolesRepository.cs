using GymInnowise.Authorization.Persistence.Models;

namespace GymInnowise.Authorization.Persistence.Repositories.Interfaces
{
    public interface IRolesRepository
    {
        Task CreateRoleAsync(Role role);
        Task DeleteRoleAsync(Role role);
        Task<Role?> GetRoleAsync(string role);
        Task<IEnumerable<RoleDto>> GetAllRolesAsync();
    }
}
