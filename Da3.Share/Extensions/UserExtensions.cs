using System.Linq;
using System.Security.Claims;

namespace Da3.Share.Extensions
{
    public static class UserExtensions
    {
        public static string GetRole(this ClaimsPrincipal user)
        {
            if (user == null)
                return "";
            return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        }
        
        public static string GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim?.Value;
        }
    }
}