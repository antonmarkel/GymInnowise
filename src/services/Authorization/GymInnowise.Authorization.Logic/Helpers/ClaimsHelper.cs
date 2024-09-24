using System.Security.Claims;

namespace GymInnowise.Authorization.Logic.Helpers
{
    public static class ClaimsHelper
    {
        public static Guid GetAccountId(IEnumerable<Claim> claims)
        {
            var accountIdClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            return Guid.Parse(accountIdClaim!.Value);
        }
    }
}
