using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Zuhid.Identity.Models;

namespace Zuhid.Identity.Repositories;

public class UserRepository(IdentityContext context, UserManager<Entities.User> userManager)
{
    public async Task<List<Models.User>> Get()
    {
        return await userManager.Users.Select(u => new Models.User
        {
            Id = u.Id,
            Email = u.Email ?? string.Empty,
            Phone = u.PhoneNumber ?? string.Empty
        }).ToListAsync().ConfigureAwait(false);
    }
}
