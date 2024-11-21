using System.Security.Claims;

namespace GymInnowise.SectionService.Logic.Features.Helpers
{
    public static class ClaimsHelper
    {
        public static Guid? GetAccountId(IEnumerable<Claim> claims)
        {
            var accountIdClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (accountIdClaim is null)
            {
                return null;
            }

            return Guid.Parse(accountIdClaim!.Value);
        }
    }
}
