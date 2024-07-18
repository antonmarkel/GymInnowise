﻿using GymInnowise.Authorization.Persistence.Models.Enities;
using GymInnowise.Authorization.Shared.Enums;

namespace GymInnowise.Authorization.Persistence.Repositories.Interfaces
{
    public interface IRolesRepository
    {
        Task<RoleEntity?> GetRoleAsync(RoleEnum role);
    }
}
