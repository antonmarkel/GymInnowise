using GymInnowise.Authorization.Persistence.Models.Enities;
using GymInnowise.Authorization.Shared.Dtos.Previews;
using GymInnowise.Authorization.Shared.Enums;

namespace GymInnowise.Authorization.Persistence.Repositories.Interfaces
{
    public interface IRolesRepository
    {
        Task CreateRoleAsync(RoleEntity role);
        Task<RoleEntity?> GetRoleAsync(RoleEnum role);
    }
}
