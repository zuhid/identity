using Microsoft.EntityFrameworkCore;
using Zuhid.BaseApi;

namespace Zuhid.Identity;

public interface IIdentityRepository {
  IQueryable<Models.User> Query { get; }
  Task<List<Models.User>> Get();
  Task<List<Models.User>> Get(Guid id);
  Task<SaveRespose> Add(Entities.User entity);
  Task<SaveRespose> Update(Entities.User entity);
  Task<int> Delete(Guid id);
}

public class IdentityRepository(IdentityContext context) : BaseRepository<IdentityContext, Models.User, Entities.User>(context), IIdentityRepository {
  public override IQueryable<Models.User> Query => context.Users.Select(entity => new Models.User {
    Id = entity.Id,
    UpdatedById = entity.UpdatedById,
    UpdatedBy = entity.UpdatedById.ToString(),
    UpdatedDate = entity.UpdatedDate,
    Email = entity.Email!,
    Password = entity.PasswordHash ?? string.Empty,
    Phone = entity.PhoneNumber ?? string.Empty,
  });

  public async Task<List<Models.User>> Get() => await Query.ToListAsync().ConfigureAwait(false);
}
