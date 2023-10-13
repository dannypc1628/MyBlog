using MyBlog.Models;
using System.Security.Claims;

namespace MyBlog.Extensions
{
    public static class ClaimsPrincipalExtenstions
    {
        public static int GetUserId(this ClaimsPrincipal claims)
        {
            if (claims.Identity.IsAuthenticated)
            {
                var idStr = claims.FindFirstValue(nameof(User.Id));
                if (int.TryParse(idStr, out int id))
                {
                    return id;
                }
            }

            return 0;
        }
    }
}
