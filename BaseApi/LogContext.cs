using Microsoft.EntityFrameworkCore;

namespace Zuhid.BaseApi;

public class LogContext(DbContextOptions<LogContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ToSnakeCase();
        builder.Entity<Log>(b => b.ToTable("log"));
    }

    public DbSet<Log> Log { get; set; }
}
