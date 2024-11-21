using GymInnowise.Authorization.Logic.Results;
using OneOf;
using OneOf.Types;

namespace GymInnowise.Authorization.Logic.Interfaces
{
    public interface IVerificationService
    {
        public const string VerificationActionName = "VerifyAction";
        Task<OneOf<Success, NotFound, Expired>> VerifyAsync(Guid token);
        void StartVerificationAsync(Guid accountId, string email);
    }
}