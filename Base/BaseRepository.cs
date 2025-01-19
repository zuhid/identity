using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Zuhid.Base;

public abstract class BaseRepository<TContext, TModel, TEntity>
  where TContext : IDbContext
  where TModel : BaseModel
  where TEntity : BaseEntity
{
    public abstract IQueryable<TModel> Query { get; }
    protected TContext context;

    public BaseRepository(TContext context) => this.context = context;

    public async Task<List<TModel>> Get(Guid id) => await Query.Where(n => n.Id.Equals(id)).ToListAsync();

    public async Task<bool> Add(TEntity entity)
    {
        entity.Updated = DateTime.UtcNow;
        context.Set<TEntity>().Add(entity);
        return (await context.SaveChangesAsync()) == 1;
    }

    public async Task<bool> Update(TEntity entity)
    {
        context.Entry(entity).Property(p => p.Updated).OriginalValue = context.Entry(entity).Property(p => p.Updated).CurrentValue;
        context.Entry(entity).Property(p => p.Updated).CurrentValue = DateTime.UtcNow;
        context.Set<TEntity>().Update(entity);
        return (await context.SaveChangesAsync()) == 1;
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
