using Microsoft.EntityFrameworkCore;
using Zuhid.Base;
using Zuhid.Identity.Entities;

namespace Zuhid.Identity;

public class IdentityContext(DbContextOptions<IdentityContext> options) : DbContext(options), IDbContext
{
    public DbSet<User> User { get; set; }
}
