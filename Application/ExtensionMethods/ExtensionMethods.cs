using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static async Task<AppUser> GetCurrentUser(this DataContext context, IUserAccessor userAccessor)
        {
           var user = await context.Users
                            .Include(u => u.Photos)
                            .FirstOrDefaultAsync(u => u.UserName == userAccessor.GetUsername());

            return user;
        }
    }
}