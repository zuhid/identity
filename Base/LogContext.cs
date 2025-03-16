using Microsoft.EntityFrameworkCore;

namespace Zuhid.Base;

public class LogContext(DbContextOptions<LogContext> options) : DbContext(options)
{
    public DbSet<Log> Log { get; set; }
}
