using GymInnowise.Authorization.Persistence.Models.Enities;
using GymInnowise.Authorization.Shared.Dtos.Previews;


namespace GymInnowise.Authorization.Persistence.Repositories.Interfaces
{
    public interface IRolesRepository
    {
        Task CreateRoleAsync(RoleEntity role);
        Task DeleteRoleAsync(RoleEntity role);
        Task<RoleEntity?> GetRoleAsync(string role);
        Task<IEnumerable<RolePreview>> GetAllRolesAsync();
    }
}
