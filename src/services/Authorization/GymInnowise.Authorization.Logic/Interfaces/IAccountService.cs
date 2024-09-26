using GymInnowise.Authorization.Logic.Services.Results;
using GymInnowise.Shared.Authorization.Dtos.RequestModels;
using OneOf;
using OneOf.Types;

namespace GymInnowise.Authorization.Logic.Interfaces
{
    public interface IAccountService
    {
        Task<OneOf<Success, AccountAlreadyExists>> RegisterAsync(RegisterRequest registerRequest);
        Task<OneOf<Success, NotFound>> UpdateAsync(Guid accountId, UpdateRequest updateRequest);
        Task<OneOf<Success, NotFound>> UpdateRolesAsync(Guid accountId, UpdateRolesRequest updateRolesRequest);
    }
}