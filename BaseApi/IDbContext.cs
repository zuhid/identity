using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Zuhid.BaseApi;

public interface IDbContext {
  DatabaseFacade Database { get; }

  DbSet<TEntity> Set<TEntity>() where TEntity : class;

  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

  EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}
