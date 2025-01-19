using Microsoft.EntityFrameworkCore;
using Zuhid.Base;

namespace Zuhid.Identity;

public class IdentityRepository(IdentityContext context) : BaseRepository<IdentityContext, Models.User, Entities.User>(context)
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
