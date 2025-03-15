using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zuhid.Identity.Entities;
using Zuhid.Base;

namespace Zuhid.Identity;

public class IdentityContext : DbContext, IDbContext
{
  public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }

  public DbSet<User> User { get; set; }

}
