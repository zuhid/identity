using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zuhid.BaseApi;
using Zuhid.Identity.Entities;

namespace Zuhid.Identity;

public class IdentityContext(DbContextOptions<IdentityContext> options) : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>(options), IDbContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ToSnakeCase();
        builder.Entity<User>(b => b.ToTable("users"));
        builder.Entity<Role>(b => b.ToTable("roles"));
        builder.Entity<UserClaim>(b => b.ToTable("user_claims"));
        builder.Entity<UserRole>(b => b.ToTable("user_roles"));
        builder.Entity<UserLogin>(b => b.ToTable("user_logins"));
        builder.Entity<RoleClaim>(b => b.ToTable("role_claims"));
        builder.Entity<UserToken>(b => b.ToTable("user_tokens"));
    }
}