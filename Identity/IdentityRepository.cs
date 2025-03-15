using Microsoft.EntityFrameworkCore;
using Zuhid.Base;
using Models = Zuhid.Identity.Models;
using Entities = Zuhid.Identity.Entities;

namespace Zuhid.Identity;

public interface IIdentityRepository
{
  IQueryable<Models.User> Query { get; }
  Task<List<Models.User>> Get();
  Task<List<Models.User>> Get(Guid id);
  Task<SaveRespose> Add(Entities.User entity);
  Task<SaveRespose> Update(Entities.User entity);
  Task<bool> Delete(Guid id);
}

public class IdentityRepository(IdentityContext context) : BaseRepository<IdentityContext, Models.User, Entities.User>(context), IIdentityRepository
{
  public override IQueryable<Models.User> Query => context.User.Select(entity => new Models.User
  {
    Id = entity.Id,
    UpdatedById = entity.UpdatedById,
    UpdatedBy = entity.UpdatedById.ToString(),
    Updated = entity.Updated,
    Email = entity.Email,
    Password = entity.Password,
    Phone = entity.Phone,
  });

  public async Task<List<Models.User>> Get() => await Query.ToListAsync();
}
