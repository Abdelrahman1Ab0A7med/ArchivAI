using System.Security.Claims;

namespace ArchivAI.Api.Extensions
{
    public static class ClaimsExtentions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var userId = user.FindFirst("id")?.Value;
            return Guid.Parse(userId);
        }
    }
}
