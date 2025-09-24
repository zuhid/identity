using Microsoft.EntityFrameworkCore;

namespace Zuhid.BaseApi;

public static class IQueryableExtension {
  public static async Task<List<T>> ToListNoWait<T>(this IQueryable<T> queryable) where T : class {
    return await queryable.ToListAsync().ConfigureAwait(false);
  }
}
