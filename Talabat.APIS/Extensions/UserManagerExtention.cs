using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entites.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Talabat.APIS.Extensions
{
    public static class UserManagerExtention
    {
        public static async Task<AppUser> FindUserWithAddressByEmailAsync(this UserManager<AppUser> userManager, ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = userManager.Users.Include(u => u.Address).FirstOrDefault(u => u.NormalizedEmail == email.ToUpper());
            return user;
        }
    }
}
