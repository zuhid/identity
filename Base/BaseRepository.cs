using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Zuhid.Base;

public abstract class BaseRepository<TContext, TModel, TEntity>(TContext context)
  where TContext : IDbContext
  where TModel : BaseModel
  where TEntity : BaseEntity
{
  public abstract IQueryable<TModel> Query { get; }

  public async Task<List<TModel>> Get(Guid id) => await Query.Where(n => n.Id.Equals(id)).ToListAsync();

  public async Task<SaveRespose> Add(TEntity entity)
  {
    entity.Updated = DateTime.UtcNow;
    context.Set<TEntity>().Add(entity);
    _ = await context.SaveChangesAsync();
    return new SaveRespose { Updated = entity.Updated };
  }

  public async Task<SaveRespose> Update(TEntity entity)
  {
    // the updated value is set to make sure no one else modifed the record since the last read
    context.Entry(entity).Property(p => p.Updated).OriginalValue = context.Entry(entity).Property(p => p.Updated).CurrentValue;
    entity.Updated = DateTime.UtcNow; // Update the record with to have the current utc value
    context.Set<TEntity>().Update(entity);
    _ = await context.SaveChangesAsync();
    return new SaveRespose { Updated = entity.Updated };
  }

  public async Task<bool> Delete(Guid id)
  {
    var entity = await context.Set<TEntity>().FirstOrDefaultAsync(m => m.Id.Equals(id));
    if (entity != null)
    {
      context.Set<TEntity>().Remove(entity);
      return (await context.SaveChangesAsync()) == 1;
    }
    return false;
  }
}
